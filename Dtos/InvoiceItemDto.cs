using Newtonsoft.Json;
using sassy.bulk.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace sassy.bulk.Dtos
{
    public class InvoiceItemDto
    {
        public string InvoiceItemId { get; set; }
        public string InvoiceNumber { get; set; }
        public string DepartmentId { get; set; }
        public string CategoryId { get; set; }
        public string ItemId { get; set; }
        public string ItemSKUId { get; set; }
        [Required(ErrorMessage = "SKUCode is required")]
        public string SKUCode { get; set; }
        public string ItemName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal ReturnQty { get; set; }
        public decimal SyncQty { get; set; }
        public SellQuantity SellQuantity { get; set; }
        [NotMapped]
        public string SellQuantityString { get { return Regex.Replace(SellQuantity.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public decimal CaseQty { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Cost { get; set; }
        public string SoldAs { get; set; }
        public string UOMId { get; set; }
        public decimal LinePrice { get; set; }
        [JsonProperty("InvoiceType")]
        public TransactionType Type { get; set; }
        [NotMapped]
        public string InvoiceTypeString { get { return Regex.Replace(Type.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public List<DiscountDto> InvoiceItemDiscounts { get; set; }
        public string OrigionalItemId { get; set; }
        public string Reason { get; set; }
        public string ReasonNote { get; set; }
        public decimal RedeemedPoints { get; set; }
        public decimal RewardDiscount { get; set; }
        public bool IsReturnable { get; set; } = true;
        public string InvoiceId { get; set; }
    }
}
