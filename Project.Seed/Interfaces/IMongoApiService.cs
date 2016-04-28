using System.Collections.Generic;

namespace Project.Seed.Interfaces
{
    public interface IMongoApiService
    {
        Mongo.MongoUser Login(CricutApi.ApiUser user);
        void SaveProjects(List<Mongo.MongoProject> projects);
        void SaveProjects(List<CricutApi.ProjectDetails> projects);
        List<Mongo.MongoProject> GetAllProjects();
        void LoadProjectDetails(Mongo.MongoProject project);
    }
}
