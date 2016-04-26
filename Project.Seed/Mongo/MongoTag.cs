namespace Project.Seed.Mongo
{
    public class MongoTag
    {
        public string TagName { get; set; }
        //public MongoTag Parent { get; set; }

        public MongoTag()
        {
        }

        public MongoTag(string tagName)
        {
            TagName = tagName;
            //if (tagName.Contains("/"))
            //{
            //    TagName = tagName.Substring(tagName.IndexOf("/") + 1);
            //    Parent = new MongoTag(tagName.Substring(0, tagName.IndexOf("/")));
            //}
        }
    }
}