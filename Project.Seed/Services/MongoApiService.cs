using System;
using System.Collections.Generic;
using Project.Seed.CricutApi;
using Project.Seed.Interfaces;
using Project.Seed.Mongo;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Project.Seed.Services
{
    public class MongoApiService : IMongoApiService
    {
        Dictionary<string, ProjectMaterials> _materialCache = new Dictionary<string, ProjectMaterials>();
        Dictionary<string, Machine> _machineCache = new Dictionary<string, Machine>();
        Dictionary<string, Software> _softwareCache = new Dictionary<string, Software>();
        HttpClient _client;
        HttpClientHandler _handler;

        public MongoApiService(string apiUrl)
        {
            var cookieContainer = new CookieContainer();
            _handler = new HttpClientHandler { CookieContainer = cookieContainer };
            _client = SetupClientWithSession(apiUrl);
            LoadMachineList();
            LoadSoftwareList();
        }

        public MongoUser Login(ApiUser user)
        {
            const string userCreationUrl = "/auth/local";
            
            var payload = new StringContent(JsonConvert.SerializeObject(user, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8, "application/json");
            var response = _client.PostAsync(userCreationUrl, payload).Result;
            var cookiesContainer = _handler.CookieContainer;
            IEnumerable<string> cookies;
            if (response.Headers.TryGetValues("set-cookie", out cookies))
            {
                foreach (var c in cookies)
                {
                    cookiesContainer.SetCookies(new Uri("http://cricut.com"), c);
                }
            }

            MongoUser mongoUser = null;
            if (response.IsSuccessStatusCode)
            {
                var me = _client.GetAsync("/user/me").Result;
                if (me.IsSuccessStatusCode)
                {
                     mongoUser = me.Content.ReadAsAsync<MongoUser>().Result;
                }
            }
            else
            {
                Console.WriteLine("Error creating user profile: {0} - {1}", user.Email, response.ReasonPhrase);
            }

            return mongoUser;
        }

        public void SaveProjects(List<MongoProject> projects)
        {
            const string projectsUrl = "/projects/";

            foreach (var project in projects)
            {
                VerifyReferences(project);
                var json = JsonConvert.SerializeObject(project,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                var response = _client.PostAsync(projectsUrl, payload).Result;
                
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Error Posting project: {0}", project.Title);
                }
            }
        }

        public string GetNewProjectId()
        {
            const string newProjectIdUrl = "/projects/newProjectId";
            var response = _client.GetAsync(newProjectIdUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<ProjectId>().Result.Id;
            }

            return null;
        }

        private void VerifyReferences(MongoProject project)
        {
            VerifyMaterials(project.MaterialsUsed.Materials.CutMaterials.Cricut);
            VerifyMachines(project.Complexity.CompatibleMachines);
            VerifySoftware(project.Complexity.CompatibleSoftware);
        }

        private void VerifyMachines(List<Machine> compatibleMachines)
        {
            foreach(var machine in compatibleMachines)
            {
                if (!MachineExists(machine))
                    AddMachine(machine);
                machine.Id = _machineCache[machine.Name].Id;
            }
        }

        private void LoadMachineList()
        {
            const string machineUrl = "/machines/";
            var response = _client.GetAsync(machineUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var machines = response.Content.ReadAsAsync<List<Machine>>().Result;
                foreach(var machine in machines)
                {
                    _machineCache.Add(machine.Name, machine);
                }
            }
        }

        private bool MachineExists(Machine machine)
        {
            return _machineCache.ContainsKey(machine.Name);
        }

        private void AddMachine(Machine machine)
        {
            const string machineUrl = "/machines/";
            var json = JsonConvert.SerializeObject(new { name = machine.Name },
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(machineUrl, payload).Result;

            if (!response.IsSuccessStatusCode) throw new Exception();

            var machineObj = response.Content.ReadAsAsync<Machine>().Result;
            _machineCache.Add(machine.Name, machineObj);
        }

        private void VerifySoftware(List<Software> compatibleSoftware)
        {
            foreach(var software in compatibleSoftware)
            {
                if (!SoftwareExists(software))
                    AddSoftware(software);
                //material.Id = _materialCache[material.Name].Id;
                //software.Id = _softwareCache[software.Name].Id;
            }
        }

        private void LoadSoftwareList()
        {
            const string softwareUrl = "/software/";
            var response = _client.GetAsync(softwareUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                var softwareList = response.Content.ReadAsAsync<List<Software>>().Result;
                foreach(var s in softwareList)
                {
                    _softwareCache.Add(s.Name, s);
                }
            }
        }

        private bool SoftwareExists(Software software)
        {
            return _softwareCache.ContainsKey(software.Name);
        }

        private void AddSoftware(Software software)
        {
            const string softwareUrl = "/software/";
            var json = JsonConvert.SerializeObject(new { name = software.Name },
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(softwareUrl, payload).Result;

            if (!response.IsSuccessStatusCode) throw new Exception();

            var s = response.Content.ReadAsAsync<Software>().Result;
            _softwareCache.Add(s.Name, s);
        }

        private void VerifyMaterials(IEnumerable<ProjectMaterials> materials)
        {
            foreach (var material in materials)
            {
                if (!MaterialExists(material.Name))
                {
                    AddMaterial(material.Name);
                }
                material.Id = _materialCache[material.Name].Id;
            }
        }

        public void SaveProjects(List<ProjectDetails> projects)
        {
            var mongoProjects = new List<MongoProject>();
            foreach (var project in projects)
            {
                // Map to Mongo Document
                mongoProjects.Add(new MongoProject(project));
            }
            SaveProjects(mongoProjects);
        }

        public bool MaterialExists(string material)
        {
            if (_materialCache.ContainsKey(material)) return true;

            var materialUrl = "/materials/" + material;
            var response = _client.GetAsync(materialUrl).Result;

            if (!response.IsSuccessStatusCode) return false;

            var materials = response.Content.ReadAsAsync<List<ProjectMaterials>>().Result;
            var m = materials.FirstOrDefault(mat => mat.Name.ToLower() == material.ToLower());
            if (m != null)
            {
                _materialCache.Add(material, m);
                return true;
            }

            return false;
        }

        public void AddMaterial(string material)
        {
            const string materialUrl = "/materials/";
            var json = JsonConvert.SerializeObject(new { name = material},
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(materialUrl, payload).Result;

            if (!response.IsSuccessStatusCode) throw new Exception();

            var m = response.Content.ReadAsAsync<ProjectMaterials>().Result;

            if (m != null)
                _materialCache.Add(material, m);
        }

        private HttpClient SetupClient(string url)
        {
            var client = new HttpClient(_handler)
            {
                BaseAddress = new Uri(url)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private HttpClient SetupClientWithSession(string url)
        {
            const string getSessionUrl = "/V4/Lookups/GetAppSessionData?appName=Kepler";

            var client = SetupClient(url);

            // Get a session
            var session = client.GetAsync(getSessionUrl).Result;
            return client;
        }

        public List<MongoProject> GetAllProjects()
        {
            const string getAllUrl = "/projects?pageSize=10000";
            var response = _client.GetAsync(getAllUrl).Result;

            if (!response.IsSuccessStatusCode) throw new Exception();

            return response.Content.ReadAsAsync<List<MongoProject>>().Result;
        }

        public void LoadProjectDetails(MongoProject project)
        {
            var projectDetailsUrl = $"/projects/{project.Id}?full=true";
            var response = _client.GetAsync(projectDetailsUrl).Result;

            if (!response.IsSuccessStatusCode) throw new Exception();

            project = response.Content.ReadAsAsync<MongoProject>().Result;
        }

        private class ProjectId
        {
            [JsonProperty("_id")]
            public string Id { get; set; }
        }
    }
}
