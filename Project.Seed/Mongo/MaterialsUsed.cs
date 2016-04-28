using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class MaterialsUsed
    {
        public string ImageUrl { get; set; }
        public MongoMaterials Materials { get; set; } = new MongoMaterials();
        //public List<ProjectMaterials> Materials { get; set; } = new List<ProjectMaterials>();

        public MaterialsUsed()
        {
        }

        public MaterialsUsed(string materialsList)
        {
            foreach(var material in materialsList.Split(','))
            {
                Materials.CutMaterials.Cricut.Add(new ProjectMaterials { Name = material });
                Materials.OtherMaterials.Other.Add(new ProjectMaterials { Name = material });
            }
        }
    }
}
