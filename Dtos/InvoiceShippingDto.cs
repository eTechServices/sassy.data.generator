namespace sassy.bulk.Dtos
{
    public class InvoiceShippingDto
    {
        public string InvoiceNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public decimal ShippingCost { get; set; }
        public string Size { get; set; }
        public string InvoiceId { get; set; }
    }
}
