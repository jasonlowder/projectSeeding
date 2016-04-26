using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class MongoComplexity
    {
        public string OverallTimeRequired { get; set; }
        public string PrepTime { get; set; }
        public string AssemblyTime { get; set; }
        public bool CanOnlyBeCustomized { get; set; }
        public bool CanOnlyBeMakeItNow { get; set; }
        public List<Machine> CompatibleMachines { get; set; } = new List<Machine>();
        public List<Software> CompatibleSoftware { get; set; } = new List<Software>();
        public Difficulty Difficulty { get; set; }
    }
}
