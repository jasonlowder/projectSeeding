using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class MongoMaterials
    {
        public Materials CutMaterials { get; set; } = new Materials();
        public Materials OtherMaterials { get; set; } = new Materials();
    }
}
