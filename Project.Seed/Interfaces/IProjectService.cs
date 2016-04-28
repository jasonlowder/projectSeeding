﻿using System.Collections.Generic;

namespace Project.Seed.Interfaces
{
    public interface IProjectService
    {
        Dictionary<CricutApi.ApiUser, List<Mongo.MongoProject>> GetAll();
        List<CricutApi.ProjectTag> GetProjectTags(int canvasId);
    }
}
