using System;
using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class StepDetails
    {
        public List<Section> Section { get; set; } = new List<Section>();

        public StepDetails()
        {
        }

        public StepDetails(List<CricutApi.ProjectStep> steps)
        {
            foreach(var step in steps)
            {
                Section.Add(new Section(step));
            }
        }
    }
}