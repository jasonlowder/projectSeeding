using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class Materials
    {
        public List<ProjectMaterials> Cricut { get; set; } = new List<ProjectMaterials>();
        public List<ProjectMaterials> Other { get; set; } = new List<ProjectMaterials>();
    }
}