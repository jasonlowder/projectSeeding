using System;

namespace Project.Seed.CricutApi
{
    public class ProjectImage
    {
        public int ProjectImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public int ImageOrder { get; set; }
        public bool IsDefault { get; set; }

        public int ProjectId { get; set; }
        public int SingleImageSetGroupId { get; set; }


        // Not currently being used
        public bool IsVideo { get; set; }
        public DateTime UploadDate { get; set; }
        public string MimeType { get; set; }
        public int YouTubeId { get; set; }
    }
}