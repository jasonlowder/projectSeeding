using Newtonsoft.Json;

namespace Project.Seed.Mongo
{
    public class MongoProfile
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public ProfileMongo Profile { get; set; }
    }
}