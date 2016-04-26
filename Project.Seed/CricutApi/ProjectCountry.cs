namespace Project.Seed.CricutApi
{
    public class ProjectCountry
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string ISO3166 { get; set; }
        public string UrlPrefix { get; set; }
        public bool Selected { get; set; }
        public int ShoppingCountryId { get; set; }
        public string LanguageCode { get; set; }
        public int CurrencyId { get; set; }
        public int StoreNumber { get; set; }
        public bool IsEnabled { get; set; }
        public int RegionId { get; set; }
    }
}