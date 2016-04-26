using System;
using System.Collections.Generic;
using Project.Seed.Interfaces;
using Project.Seed.Mongo;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using Project.Seed.CricutApi;

namespace Project.Seed
{
    public class MongoApi : IMongoApiService
    {
        public MongoUser Login(ApiUser user)
        {
            throw new NotImplementedException();
        }

        public void SaveProjects(List<ProjectDetails> projects)
        {
            throw new NotImplementedException();
        }

        public void SaveProjects(List<MongoProject> projects)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000");

            foreach (var project in projects)
            {
                // Add document to Mongo via api2
                var json = JsonConvert.SerializeObject(project, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                var payload = new StringContent(json, Encoding.UTF8, "application/json");

                var postResult = client.PostAsync("/api/projects", payload).Result;
                //var postContent = postResult.Content.ReadAsStringAsync().Result;
                Console.WriteLine(postResult.IsSuccessStatusCode);
            }
        }
    }
}
