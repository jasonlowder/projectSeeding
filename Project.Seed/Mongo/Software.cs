using Newtonsoft.Json;

namespace Project.Seed.Mongo
{
    public class Software
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
