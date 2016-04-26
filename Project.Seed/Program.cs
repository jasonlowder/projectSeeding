using System;
using System.Collections.Generic;
using System.Linq;
using Project.Seed.Services;

namespace Project.Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            var recordCount = 10;
            var max = 5000;
            for (var startIndex = 0; startIndex < max; startIndex += recordCount)
            {
                var projects = GetProjectViaApi(startIndex + recordCount, startIndex);
                UploadToMongo(projects);
                Console.WriteLine("Projects saved: {0}", startIndex + recordCount);
            }

            Console.WriteLine("End");
            Console.ReadKey();
        }

        private static void UploadToMongo(Dictionary<CricutApi.ApiUser, List<Mongo.MongoProject>> projects)
        {
            const string awsHost = "https://s3-us-west-2.amazonaws.com";
            const string awsBucket = "community-public-dev";
            var mongoService = new Services.MongoApiService("http://us-dev2-api.cricut.com"); // localhost:3000");
            foreach (var user in projects.Keys)
            {
                var mongoUser = mongoService.Login(user);
                if (mongoUser != null)
                {
                    var userProjects = projects[user];
                    foreach (var project in userProjects)
                    {
                        project.ProfileId = mongoUser.User.Profile.Id;

                        project.Id = mongoService.GetNewProjectId();
                        if (string.IsNullOrEmpty(project.Id))
                        {
                            throw new Exception($"Could not get a project Id for '{project.Title}'");
                        }

                        // move images to AWS S3
                        var uploader = new AmazonUploader();
                        foreach (var image in project.ProjectImages)
                        {
                            uploader.SendFileToS3(image.ImageUrl,
                                                  awsBucket,
                                                  $"/users/{project.ProfileId}/projects/{project.Id}",
                                                  image.ImageUrl.Split('/').Last());
                            image.ImageUrl = $"{awsHost}/{awsBucket}/users/{project.ProfileId}/projects/{project.Id}/{image.ImageUrl.Split('/').Last()}";
                        }
                    }
                    mongoService.SaveProjects(userProjects);
                }
            }
        }

        private static Dictionary<CricutApi.ApiUser, List<Mongo.MongoProject>> GetProjects(int resultCount, int startIndex)
        {
            var projectService = new Services.EFProjectService(resultCount, startIndex);
            return projectService.GetAll();
        }

        private static Dictionary<CricutApi.ApiUser, List<Mongo.MongoProject>> GetProjectViaApi(int resultCount, int startIndex)
        {
            var projectService = new Services.ApiProjectService(resultCount, startIndex);
            return projectService.GetAll();
        }
    }
}
