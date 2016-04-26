using Newtonsoft.Json;

namespace Project.Seed.Mongo
{
    public class ProfileMongo
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}