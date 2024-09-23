namespace sassy.bulk.Dtos
{
    public class InvoiceOtherChargesDto
    {
        public string InvoiceNumber { get; set; }
        public string TenderId { get; set; }
        public string ChargeId { get; set; }
        public string ChargeType { get; set; }
        public decimal Charge { get; set; }
        public decimal ChargeAppliedAmount { get; set; }
        public decimal ChargedAmount { get; set; }
        public string ChargeName { get; set; }
        public string ChargeReceiptNote { get; set; }
        public string ChargeReceiptTitle { get; set; }
        public string InvoiceId { get; set; }
    }
}
