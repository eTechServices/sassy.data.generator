namespace sassy.bulk.Enums
{
    public class DiscountDto
    {
        public string InvoiceNumber { get; set; }
        public DiscountType DiscountType { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public decimal Percent { get; set; }
        public string DiscountId { get; set; }
        public string DiscountName { get; set; }
        public string DiscountCode { get; set; }
        public string CustomerId { get; set; }
        public string LocationId { get; set; }
        public string RegisterId { get; set; }
        public bool IsOnlineOrder { get; set; }
    }
}
