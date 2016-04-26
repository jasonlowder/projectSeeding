using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class Section
    {
        public string Title { get; set; }
        public List<MongoStep> Steps { get; set; } = new List<MongoStep>();

        public Section()
        {
        }

        public Section(CricutApi.ProjectStep step)
        {
            Steps.Add(new MongoStep(step));
        }
    }
}