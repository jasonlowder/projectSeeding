using System;
using System.Collections.Generic;

namespace Project.Seed.CricutApi
{
    public class ProjectDetails
    {
        public string OtherMaterials { get; set; }
        public string TimeRequired { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ScreenName { get; set; }
        public string ImageOrder { get; set; }
        public string Faves { get; set; }
        public string SkillLevel { get; set; }
        public bool InAccess { get; set; }
        public int CanvasId { get; set; }
        public string ExtendedAttributes { get; set; }
        public string ProfileId { get; set; }
        public string StatusId { get; set; }
        public string ProjectUrl { get; set; }
        public string DifficultyId { get; set; }
        public string TimeRequiredId { get; set; }
        public string Keywords { get; set; }
        public string IsFeatured { get; set; }
        public string CategoryId { get; set; }
        public List<ProjectImage> ProjectImages { get; set; }
        public List<ProjectStep> StepDetails { get; set; }
        public List<ProjectTag> TagDetails { get; set; }
        public List<EntitlementMethod> EntitlementMethods { get; set; }
        public int? ProductId { get; set; }
        public string CutFileUrl { get; set; }
        public string CutMimeType { get; set; }
        public string CutFileName { get; set; }
        public string Category { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Difficulty { get; set; }
        public decimal ProjectTotal { get; set; }
        public string ProjectTotalView { get; set; }
        public ProjectCountry Country { get; set; }
        public bool? MakeItNow { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string OverviewDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbUrl { get; set; }




        // Not currently being used
        public DateTime CreationDate { get; set; }
        public int RowNumber { get; set; }
        public int DefaultImageId { get; set; }
    }
}