using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using sassy.bulk.Enums;

namespace sassy.bulk.Dtos
{
    public class InvoiceTenderDto
    {
        public string Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public TenderType TenderType { get; set; }
        [NotMapped]
        public string TenderTypeString { get { return Regex.Replace(TenderType.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal CCFee { get; set; }
        public string Remarks { get; set; } = "";
        public decimal Change { get; set; }
        public string Status { get; set; } = "Done";
        public string InvoiceId { get; set; }
        public decimal TipAmount { get; set; }
        public decimal CashDiscount { get; set; }
    }
}
