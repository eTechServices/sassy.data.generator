using Newtonsoft.Json;
using sassy.bulk.ExtensionsUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using sassy.bulk.Enums;

namespace sassy.bulk.Dtos
{
    public class SaleInvoiceDto
    {
        [JsonProperty("orderNo")]
        public int OrderNo { get; set; } = 1;
        [JsonProperty("invoiceNumber")]
        public string InvoiceNumber { get; set; }
        public string RefInvoiceNumber { get; set; }
        public string ReturnInvoiceNumbers { get; set; }
        public IEnumerable<string> ReturnedInvoices { get => ReturnInvoiceNumbers.ToEnumerable(); }
        [JsonProperty("customerId")]
        public string CustomerId { get; set; } = "OTM2NDQ2NjY5MA==";
        [JsonProperty("customerName")]
        public string CustomerName { get; set; } = "A ALVARADO-HERNANDEZ";
        [JsonProperty("customerPhone")]
        public string CustomerPhone { get; set; } = "9364466690";
        [JsonProperty("locationId")]
        public string locationId { get; set; } = "1670001";
        [JsonProperty("locationName")]
        public string LocationName { get; set; } = "Bold Vapes- Camp";
        [JsonProperty("registerId")]
        public string RegisterId { get; set; } = "1670001001";
        [JsonProperty("registerName")]
        public string RegisterName { get; set; } = "Judaic";
        [JsonProperty("userId")]
        public string UserId { get; set; } 
        public string UserName { get; set; }
        public bool IsReturned { get; set; } = false;
        public bool TaxExempt { get; set; } = false;
        public PostedStatus PostedStatus { get; set; }
        [NotMapped]
        public string PostedStatusString { get { return Regex.Replace(PostedStatus.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Delivered;
        [NotMapped]
        public string ShippingStatusString { get { return Regex.Replace(ShippingStatus.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public TransactionType InvoiceType { get; set; }
        [NotMapped]
        public string InvoiceTypeString { get { return Regex.Replace(InvoiceType.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public decimal ShippingCost { get; set; } = decimal.Zero;
        public decimal SubTotal { get; set; } = decimal.Zero;
        public decimal Discount { get; set; } = decimal.Zero;
        public decimal SalesTax { get; set; } = decimal.Zero;
        public decimal GrandTotal { get; set; } = decimal.Zero;
        public decimal TaxRate { get; set; } = decimal.Zero;
        public decimal LoyaltyAward { get; set; } = decimal.Zero;
        public decimal TaxableTotals { get; set; } = decimal.Zero;
        public decimal NonTaxableTotals { get; set; } = decimal.Zero;
        public DateTime Started { get; set; } = DateTime.UtcNow;
        public DateTime Ended { get; set; } = DateTime.UtcNow;
        public DateTime Dated { get; set; } = DateTime.UtcNow;
        public decimal TenderedAmount { get; set; } = decimal.Zero;
        public decimal CashAmount { get; set; } = decimal.Zero;
        public decimal CreditCardAmount { get; set; } = decimal.Zero;
        public decimal OtherChargesAmount { get; set; } = decimal.Zero;
        public decimal RewardDiscount { get; set; } = decimal.Zero;
        [JsonProperty("InvoiceOtherCharges")]
        public List<InvoiceOtherChargesDto> InvoiceOtherCharges { get; set; }
        public decimal TotalPaid { get; set; } = decimal.Zero;
        public decimal Change { get; set; } = decimal.Zero;
        public bool PaymentFinalized { get; set; } = false;
        public bool IsOnlineOrder { get; set; } = false;
        public int TotalSaleItems { get; set; }
        public int TotalVoidItems { get; set; }
        [JsonProperty("invoiceItems")]
        public List<InvoiceItemDto> InvoiceItems { get; set; }
        [JsonProperty("discounts")]
        public List<InvoiceDiscountDto> InvoiceDiscounts { get; set; }
        [JsonProperty("tenders")]
        public List<InvoiceTenderDto> InvoiceTenders { get; set; }
        [JsonProperty("productNotes")]
        public List<InvoiceProductNoteDto> InvoiceProductNotes { get; set; }
        [JsonProperty("electronicTenders")]
        public List<ElectronicTenderDto> ElectronicTenders { get; set; }
        [JsonProperty("invoiceShipping")]
        public List<InvoiceShippingDto> InvoiceShipping { get; set; }
        [JsonProperty("invoiceBilling")]
        public List<InvoiceBillingDto> InvoiceBilling { get; set; }
        [JsonProperty("receiptRecords")]
        public List<ReceiptRecordDto> ReceiptRecords { get; set; }
        public decimal PointsAwarded { get; set; } = decimal.Zero;
        public decimal ShippingTax { get; set; } = decimal.Zero;
        public decimal CurrentPoints { get; set; } = decimal.Zero;
        public decimal RedeemedPoints { get; set; } = decimal.Zero;
        public string InvoiceNote { get; set; } = "";
        public string CustomerNote { get; set; } = "";
        public decimal TipAmount { get; set; } = decimal.Zero;
        public decimal CashDiscount { get; set; } = decimal.Zero;
    }
}
