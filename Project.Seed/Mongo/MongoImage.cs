namespace Project.Seed.Mongo
{
    public class MongoImage
    {
        public string ImageUrl { get; set; }
        public int ImageOrder { get; set; }

        public MongoImage()
        {
        }

        public MongoImage(CricutApi.ProjectImage image)
        {
            ImageUrl = image.ImageUrl;
            ImageOrder = image.ImageOrder;
        }
    }
}
