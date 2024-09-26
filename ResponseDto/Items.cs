using Newtonsoft.Json;
using sassy.bulk.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static Bogus.DataSets.Name;
using System.Text.RegularExpressions;

namespace sassy.bulk.ResponseDto
{
    public class Items
    {
        public string Id { get; set; }
        public DateTime CreatedDate {  get; set; }
        public string ItemName { get; set; }
        public string ItemLocalName { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string VendorId { get; set; }
        public string Vendor { get; set; }
        public string VendorPart { get; set; }
        public string ManufacturerId { get; set; }
        public string Manufacturer { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public ItemType ItemType { get; set; }
        public string SoldAs { get; set; }
        public AgeRestriction AgeRestriction { get; set; }
        public Gender Gender { get; set; }
        public Discount AllowDiscount { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsReturnable { get; set; } = true;
        public bool TrackStock { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool AllowNegativeStock { get; set; } = true;
        public bool IsOpnDept { get; set; }
        public string Notes { get; set; } = "";
        public bool ShowNotesWhen { get; set; }
        public bool PrintOnInvoice { get; set; } = true;
        public string Image { get; set; } = "";
        [NotMapped]
        public string ImageThumbnail
        {
            get
            {
                try
                {
                    return Image?.Replace("Products/", "Products/Thumbnail/")?.Replace("Products%5C", "Products/Thumbnail/") ?? "";
                }
                catch (System.Exception)
                {
                    Image = "";
                }
                return "";
            }
        }
        public string DescriptionURL { get; set; }
        public bool PrintAsQRCodeOnReceipt { get; set; }
        public DateTime LastEdited { get; set; } = DateTime.UtcNow;
        public string EditedBy { get; set; }
        public string AddedBy { get; set; }
        public string UnitId { get; set; }
        public string Unit { get; set; }
        public Grouping Grouping { get; set; }
        [JsonProperty("CRSItemSKUs")]
        public List<ItemSKU> ItemSKUs { get; set; }
        public List<VariantAttribute> VariantAttributes { get; set; }
        [NotMapped]
        public List<OpeningStock> OpeningStocks { get; set; }
        public List<ItemPicture> ItemPictures { get; set; }
        public bool SellOnline { get; set; }
        public bool IsDeleted { get; set; }
        public string ProductSellType { get; set; }
        public string Tags { get; set; } = "";
        public bool IsFreeShipping { get; set; }
    }
    public class ItemPicture
    {
        public int OrderNo { get; set; }
        public string Picture { get; set; }
        [JsonIgnore]
        [ForeignKey("ItemsId")]
        public Items Item { get; set; }
        public string ItemsId { get; set; }
    }
    public class ItemSKU
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string ProductCode { get; set; }
        public string SKUCode { get; set; }
        public string Barcode { get; set; }
        public string ManufacturerBarcode { get; set; }
        public string SKUType { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePriceA { get; set; }
        public decimal SalePriceB { get; set; }
        public decimal SalePriceC { get; set; }
        public decimal DiscountPrice { get; set; }
        public TaxRate TaxRateId { get; set; }
        public DateTime LastEdited { get; set; } = DateTime.Now;
        public bool Taxable { get; set; }
        public SellQuantity SellQuantity { get; set; }
        public decimal CaseQty { get; set; } = 1;
        public bool IsVisible { get; set; } = true;
        public bool IsDefault { get; set; } = true;
        public string UOMId { get; set; }
        public string UOMDescription { get; set; }
        public DefaultPrice DefaultPrice { get; set; }
        public decimal OpeningStock { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        [ForeignKey("Id")]
        public Items Items { get; set; }
        public ItemShippingDimention ShippingDimention { get; set; }
        public int ReorderLevel { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int MinOrderQty { get; set; }
        public int MaxOrderQty { get; set; }
        public List<StockInHand> StockInHands { get; set; }
        public List<StockTransaction> StockTransaction { get; set; }
        public List<StockAdjustment> StockAdjustments { get; set; }
        public bool AutoPurchaseOrder { get; set; }
        [JsonIgnore]
        public string AltBarcodes { get; set; }
        [JsonIgnore]
        public string AltSkus { get; set; }
        [JsonIgnore]
        public string MetaTags { get; set; }



        /**
            * @Author: Ebad Hassan
            * @Date:   2024-07-26 13:03:15
            * @Last Modified by:   Ebad Hassan
            * @Last Modified time: 2024-08-08 10:05:40
            * @Summary: Temporary storage for barcodes entered by the user before saving.
            * @Summary: This list is not persisted to the database.
        */
        [NotMapped]
        private List<string> TempBarcodes { get; set; } = new List<string>();
        [NotMapped]
        private List<string> TempSkus { get; set; } = new List<string>();
        [NotMapped]
        private List<string> TempTags { get; set; } = new List<string>();
        [NotMapped]
        [JsonProperty("barcodes")]
        public List<string> Barcodes
        {
            get => string.IsNullOrWhiteSpace(this.AltBarcodes) ? this.TempBarcodes : this.AltBarcodes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            set { this.TempBarcodes = value; }
        }
        [NotMapped]
        [JsonProperty("skus")]
        public List<string> Skus
        {
            get => string.IsNullOrWhiteSpace(this.AltSkus) ? this.TempSkus : this.AltSkus.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            set { this.TempSkus = value; }
        }
        [NotMapped]
        [JsonProperty("tags")]
        public List<string> Tags
        {
            get => string.IsNullOrWhiteSpace(this.MetaTags) ? this.TempTags : this.MetaTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            set { this.TempTags = value; }
        }
        public bool IsContainDuplicates() => Barcodes.GroupBy(x => x).Any(g => g.Count() > 1) || Skus.GroupBy(x => x).Any(g => g.Count() > 1);
    }
    public class StockTransaction
    {
        public string ProductId { get; set; }
        public string ItemSKUId { get; set; }
        public string ProductCode { get; set; }
        public string ProductSKUCode { get; set; }
        public string WareHouseId { get; set; }
        public decimal Quantity { get; set; }
        public decimal InhandQuantity { get; set; }
        public string InvoiceNumber { get; set; }
        public bool IsOnlineOrder { get; set; }
        public TransactionType Type { get; set; }
        [NotMapped]
        public string TypeString { get { return Regex.Replace(Type.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        public string AddedBy { get; set; }
        [ForeignKey("ItemSKUId")]
        [JsonIgnore]
        public ItemSKU ItemSKU { get; set; }
    }
    public class StockAdjustment
    {
        public string StockTransactionId { get; set; }
        public string ProductId { get; set; }
        public string ItemSKUId { get; set; }
        public string ProductCode { get; set; }
        public string ProductSKUCode { get; set; }
        public string WareHouseId { get; set; }
        public decimal Quantity { get; set; }
        public decimal InhandQuantity { get; set; }
        public decimal UnitCost { get; set; }
        public string Reason { get; set; }
        public string InvoiceNumber { get; set; }
        public AdjustmentType AdjustmentType { get; set; }
        public string AddedBy { get; set; }
        public DateTime Dated { get; set; }
        [NotMapped]
        public string AdjustmentTypeString { get { return Regex.Replace(AdjustmentType.ToString(), "([a-z])([A-Z])", "$1 $2"); } }
        [ForeignKey("ItemSKUId")]
        [JsonIgnore]
        public ItemSKU ItemSKU { get; set; }
    }
    public class StockInHand
    {
        public string ProductCode { get; set; }
        [NotMapped]
        public string ProductName { get; set; } = "";
        public string ProductSKUCode { get; set; }
        public string WareHouseId { get; set; }
        public decimal Quantity { get; set; }

        public string ProductId { get; set; }
        public string ItemSKUId { get; set; }
        public DateTime Modified { get; set; } = DateTime.UtcNow;
        [ForeignKey("ItemSKUId")]
        [JsonIgnore]
        public ItemSKU ItemSKU { get; set; }
    }

    public class ProductComposition
    {
        public string ProductCode { get; set; }
        public string PSKUId { get; set; }
        public decimal Quantity { get; set; }
        public string UOM { get; set; }
    }
    public class VariantAttribute 
    {
        public string ProductCode { get; set; }
        public string Attribute { get; set; }
        public List<string> Tags { get; set; }
        public string Tag { get; set; }
    }

    public class OpeningStock 
    {
        public string LocationId { get; set; }
        public string SKUCode { get; set; }
        public decimal Quantity { get; set; }
        public string ProductId { get; set; }
        public string ItemSKUId { get; set; }
    }

    public class ItemShippingDimention 
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        [ForeignKey("ItemSKUId")]
        public ItemSKU ItemSKU { get; set; }
        public string ItemId { get; set; }
        public string ItemSKUId { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Weight { get; set; }
        public ShippingWeightUOM WeightUOM { get; set; }
        [NotMapped]
        public string WeightUOMString { get { return WeightUOM.ToString(); } set { WeightUOM = (ShippingWeightUOM)Enum.Parse(typeof(ShippingWeightUOM), value); } }
        public ShippingDimensionUOM DimensionUOM { get; set; }
        [NotMapped]
        public string DimensionUOMString { get { return DimensionUOM.ToString(); } set { DimensionUOM = (ShippingDimensionUOM)Enum.Parse(typeof(ShippingDimensionUOM), value); } }
        [NotMapped]
        public decimal WeightInPounds { get; set; }
        [NotMapped]
        public decimal HeightInInches { get; set; }
        [NotMapped]
        public decimal LengthInInches { get; set; }
        [NotMapped]
        public decimal WidthInInches { get; set; }
    }
    public enum ShippingWeightUOM
    {
        Pound,
        Gram,
        Kilogram,
        Ounce
    }
    public enum ShippingDimensionUOM
    {
        Meter,
        Centimeter,
        Millimeter,
        Inch,
        Yard,
    }
    public enum ItemType
    {
        Sale = 1,
        Service = 2
    }
    public enum Grouping
    {
        Single = 1,
        //Variable = 2,
        //Composite = 3,
    }
    public enum AgeRestriction
    {
        None = 1,
        MustBe18 = 2,
        MustBe21 = 3,
    }
    public enum Discount
    {
        No = 1,
        Yes = 2,
    }
    public enum TaxRate
    {
        TaxRate1 = 1,
        TaxRate2 = 2,
        TaxRate3 = 3,
    }
    public enum DefaultPrice
    {
        SellPriceA = 1,
        SellPriceB = 2,
        SellPriceC = 3,
    }
    public enum AdjustmentType
    {
        All = -1,
        Adjustment = 1,
        AutoAdjustment = 2,
        ImportInventoryAdjustment = 3,
        StockTransferIn = 4,
        StockTransferOut = 5,
        DirectPurchase = 6,
        InventoryCount = 7,
        Damage = 8,
        ReturnToSender = 9,
        Lost = 10,
        Expired = 11,
        Exchange = 12,
        Other = 13
    }
}
