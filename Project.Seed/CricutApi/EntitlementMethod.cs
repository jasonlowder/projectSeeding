using System;

namespace Project.Seed.CricutApi
{
    public class EntitlementMethod
    {
        public int EntitlementMethodId { get; set; }
        public decimal PriceRetail { get; set; }
        public decimal PriceSale { get; set; }
        public int Duration { get; set; }
        public string EntitlementMethodName { get; set; }
        public string Description { get; set; }
        public EntitlementType EntitlementType { get; set; }
        public Region Region { get; set; }
        public DurationType DurationType { get; set; }
        public int ShoppingProductId { get; set; }
        public int EntitlementXid { get; set; }
        public string GroupPreviewUrl { get; set; }
        public int CurrencyId { get; set; }
        public bool Entitled { get; set; }
        public string ShoppingSku { get; set; }
        public int ImageSetGroupId { get; set; }
        public string LanguageCode { get; set; }
        public int KeplerFontId { get; set; }
        public bool IsActive { get; set; }
        public decimal ApplePrice { get; set; }
        public int EntTypeId { get; set; }
        public string EntitlementLabel { get; set; }
        public bool InSubscription { get; set; }
        public bool InAccess { get; set; }
        public string ImagePreviewUrl { get; set; }
        public int ImageSetId { get; set; }
        public string ImageSetName { get; set; }
        public int ImageId { get; set; }
        public string ImageName { get; set; }

        public Product Product { get; set; }
        public string Sku { get; set; }
        public bool Taxable { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemPriceView { get; set; }
        public string ApplePriceView { get; set; }
        public string ImageUrl { get; set; }
        public bool Included { get; set; }
        public bool ImageGroupPurchase { get; set; }
        public int ImageImportTypeId { get; set; }
        public bool IsSubscription { get; set; }
        public bool AllowRemove { get; set; } // Depreciated
        public double VatRate { get; set; }
        public double VatAmount { get; set; }
        public bool InValid { get; set; }
        public bool Valid { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string SubscriptionType { get; set; }
        public bool Available { get; set; }
        public string Message { get; set; }
        public bool Recurring { get; set; }
        public int SubscriptionId { get; set; }
        public bool IsFont { get; set; }
        public int EntitledBy { get; set; }
        public string Name { get; set; }









        public string PreviewUrl { get; set; }
        public bool IsPrintable { get; set; }

    }
}