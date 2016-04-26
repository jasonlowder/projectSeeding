﻿using System;
using System.Collections.Generic;
using Project.Seed.Interfaces;
using Project.Seed.Mongo;
using System.Net.Http;
using Project.Seed.CricutApi;
using System.Linq;
using System.Net.Http.Headers;

namespace Project.Seed.Services
{
    public class ApiProjectService : IProjectService
    {
        const string getLatestProjectsFormat = "/V4/projects/GetProjectsFeaturedLatest";
        const string getProjectDetailsFormat = "/V4/projects/GetProjectDetailsV2?projectID=";
        const string getSessionUrl = "/V4/Lookups/GetAppSessionData?appName=Kepler";
        const string baseUrl = "https://us-api.cricut.com";

        int _resultCount = 10000;
        int _startIndex = 0;

        public ApiProjectService()
        {
        }

        public ApiProjectService(int resultCount, int startIndex)
        {
            _resultCount = resultCount;
            _startIndex = startIndex;
        }

        public Dictionary<CricutApi.ApiUser, List<Mongo.MongoProject>> GetAll()
        {
            var client = SetupClientWithSession(baseUrl);
            var projects = GetProjects(client, _resultCount, _startIndex);
            return MapToUsers(projects);
        }

        private Dictionary<CricutApi.ApiUser, List<Mongo.MongoProject>> MapToUsers(List<ProjectDetails> projects)
        {
            var canvasIds = new List<int>();
            foreach (var project in projects)
            {
                canvasIds.Add(project.CanvasId);
            }

            var userService = new UserService();
            var projectIdMap = userService.GetUserDetails(canvasIds);

            var map = new Dictionary<CricutApi.ApiUser, List<Mongo.MongoProject>>();
            foreach (var user in projectIdMap.Keys)
            {
                var projectList = new List<Mongo.MongoProject>();
                foreach (var id in projectIdMap[user])
                {
                    var details = projects.Single(p => p.CanvasId == id);
                    projectList.Add(new MongoProject(details));
                }
                map.Add(user, projectList);
            }

            return map;
        }

        private List<ProjectDetails> GetProjects(HttpClient client, int resultCount, int startIndex)
        {
            // List data response.
            var urlParameters = $"?categoryID=0&resultCount={resultCount}&startIndex={startIndex}";

            // Get project list
            var response = client.GetAsync(getLatestProjectsFormat + urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<List<ProjectDetails>>().Result;
                return GetProjectDetails(dataObjects, client);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }

        private List<ProjectDetails> GetProjectDetails(List<ProjectDetails> projects, HttpClient client)
        {
            var projectList = new List<ProjectDetails>();
            foreach (var project in projects)
            {
                // Get Project Details
                var details = client.GetAsync(getProjectDetailsFormat + project.ProjectId).Result;
                if (details.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var detailsObject = details.Content.ReadAsAsync<ProjectDetails>().Result;

                    projectList.Add(detailsObject);
                }
                else
                {
                    Console.WriteLine("Error getting details for {0}", project.ProjectId);
                    projectList.Add(project);
                }
            }
            return projectList;
        }

        private HttpClient SetupClientWithSession(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Get a session
            var session = client.GetAsync(getSessionUrl).Result;
            return client;
        }
    }
}
