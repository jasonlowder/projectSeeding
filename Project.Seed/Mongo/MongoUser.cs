using Newtonsoft.Json;

namespace Project.Seed.Mongo
{
    public class MongoUser
    {
        public MongoProfile User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
    }
}
