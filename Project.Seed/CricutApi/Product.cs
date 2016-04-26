using System;

namespace Project.Seed.CricutApi
{
    public class Product
    {
        public int ProductId { get; set; }
        public int? StoreNumber { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public int? ProductTypeId { get; set; }
        public double? Price { get; set; }
        public double? RetailPrice { get; set; }
        public double? WholesalePrice { get; set; }
        public double? BasePrice { get; set; }
        public double? SavingsAmount { get; set; }
        public bool? Taxable { get; set; }
        public int? SalesVatId { get; set; }
        public DateTime? SalesStartDate { get; set; }
        public DateTime? SaleEndDate { get; set; }
        public double? SalePrice { get; set; }
        public double? AvgReview { get; set; }
        public bool? IsSoftProduct { get; set; }
        public bool? IsListable { get; set; }
        public bool IsActive { get; set; }
        public bool IsSoftDeleted { get; set; }
        public int? EntitlementMethodId { get; set; }
        public int? Quantity { get; set; }
    }
}