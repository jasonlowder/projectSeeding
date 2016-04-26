using Newtonsoft.Json;

namespace Project.Seed.Mongo
{
    public class Machine
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}