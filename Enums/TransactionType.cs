using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionType
    {
        Sale = 1,
        Refund = 2,
        Void = 3,
        DirectPurchase = 4,
        GRN = 5,
        Adjustment = 6,
        ResetInventoryCount = 7,
        StockTransferIn = 8,
        OpeningStock = 9,
        StockTransferOut = 10,
        InventoryCount = 11,
        Damage = 12,
        ReturnToSender = 13,
        Lost = 14,
        Expired = 15,
        Exchange = 16,
        Other = 17,
    }
}
