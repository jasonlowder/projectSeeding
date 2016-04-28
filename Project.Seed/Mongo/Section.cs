using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class Section
    {
        public string TitleId { get; set; }
        public List<MongoStep> Steps { get; set; } = new List<MongoStep>();
        public string Tip { get; set; }

        public Section()
        {
        }

        public Section(CricutApi.ProjectStep step)
        {
            Steps.Add(new MongoStep(step));
            TitleId = step.Title;
        }
    }
}