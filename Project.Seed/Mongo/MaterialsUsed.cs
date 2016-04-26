using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class MaterialsUsed
    {
        public string ImageUrl { get; set; }
        public List<ProjectMaterials> Materials { get; set; } = new List<ProjectMaterials>();

        public MaterialsUsed(string materialsList)
        {
            foreach(var material in materialsList.Split(','))
            {
                Materials.Add(new ProjectMaterials { Name = material });
            }
        }
    }
}
