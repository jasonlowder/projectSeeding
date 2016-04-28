using Newtonsoft.Json;
using Project.Seed.CricutApi;
using System;
using System.Collections.Generic;

namespace Project.Seed.Mongo
{
    public class MongoProject
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Quote { get; set; }
        public List<MongoImage> ProjectImages { get; set; } = new List<MongoImage>();
        public MongoComplexity Complexity { get; set; }
        public MaterialsUsed MaterialsUsed { get; set; }
        public ProjectInstructions Instructions { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public int CanvasId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public bool HasAcceptedTermsAndConditions { get; set; }
        public List<ProjectResource> Resources { get; set; } = new List<ProjectResource>();
        public string ProfileId { get; set; }
        public bool Published { get; set; } = true;
        public bool Shared { get; set; } = true;
        public string Type { get; set; } = "Cricut";
        public string DesignSpaceLink
        {
            get
            {
                return $"https://us.cricut.com/design/#/canvas/{CanvasId}";
            }
        }

        public MongoProject()
        {
        }

        public MongoProject(ProjectDetails project)
        {
            Title = project.Title;
            CanvasId = project.CanvasId;
            ModifiedDate = project.UpdateDate;
            PublishDate = project.PublishDate;
            CreatedDate = project.CreationDate;
            ProfileId = project.ProfileId;

            Complexity = new MongoComplexity
            {
                OverallTimeRequired = project.TimeRequired,
                Difficulty = new Difficulty { SkillLevel = project.DifficultyId }
            };

            MaterialsUsed = new MaterialsUsed(project.OtherMaterials);

            Instructions = new ProjectInstructions(project.StepDetails);
            Instructions.Description = project.OverviewDescription;

            foreach (var image in project.ProjectImages)
            {
                ProjectImages.Add(new MongoImage(image));
            }
            
            foreach(var tag in project.TagDetails)
            {
                //Tags.Add(new MongoTag(tag.TagName));
                Tags.Add(tag.TagName);
            }
            
            foreach(var entitlementMethod in project.EntitlementMethods)
            {
                Resources.Add(new ProjectResource(entitlementMethod));
            }

            using (var db = new CricutProjectEngineEntities())
            {
                int id;
                if (int.TryParse(project.StatusId, out id))
                {
                    Status = db.ProjectStatus.Find(id).Name;
                }
            }
        }
    }
}
