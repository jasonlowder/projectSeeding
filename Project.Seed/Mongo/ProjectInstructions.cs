using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class ProjectInstructions
    {
        public string Description { get; set; }
        public List<Section> Sections { get; set; } = new List<Section>();
        public List<string> Tips { get; set; } = new List<string>();

        public ProjectInstructions()
        {
        }

        public ProjectInstructions(List<CricutApi.ProjectStep> steps)
        {
            foreach (var step in steps)
            {
                Sections.Add(new Section(step));
            }
        }
    }
}