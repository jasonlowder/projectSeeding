namespace Project.Seed.Mongo
{
    public class ProjectResource
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        //public int KeplerFontId { get; set; }
        public string ImageUrl { get; set; }
        //public bool InAccess { get; set; }
        //public bool IsFont { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }

        public ProjectResource()
        {
        }

        public ProjectResource(CricutApi.EntitlementMethod entitlementMethod)
        {
            ImageId = entitlementMethod.ImageId;
            ImageName = entitlementMethod.ImageName;
            //KeplerFontId = entitlementMethod.KeplerFontId;
            ImageUrl = entitlementMethod.ImageUrl;
            //InAccess = entitlementMethod.InAccess;
            //IsFont = entitlementMethod.IsFont;
            //Name = entitlementMethod.Name;
            //Description = entitlementMethod.Description;
        }
    }
}