using Newtonsoft.Json;

namespace Project.Seed.Mongo
{
    public class ProjectMaterials
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}