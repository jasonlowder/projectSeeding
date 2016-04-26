using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class MongoStep
    {
        public string Description { get; set; }
        public int Order { get; set; }
        public List<StepImage> StepImages { get; set; } = new List<StepImage>();

        public MongoStep()
        {
        }

        public MongoStep(CricutApi.ProjectStep step)
        {
            Description = string.Format("{0} {1}", step.Title, step.Description);
            Order = step.Order;
        }
    }
}
