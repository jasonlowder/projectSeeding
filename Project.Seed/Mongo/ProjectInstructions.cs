using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class ProjectInstructions
    {
        public string Blurb { get; set; }
        public StepDetails StepDetails { get; set; }
        public List<string> Tips { get; set; } = new List<string>();

        public ProjectInstructions()
        {
        }

        public ProjectInstructions(List<CricutApi.ProjectStep> steps)
        {
            StepDetails = new StepDetails(steps);
        }
    }
}