using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TenderType
    {
        None = 0,
        Cash = 1,
        CashRefund = 2,
        CreditCard = 3,
        DebitCard = 4,
        CreditCardRefund = 5,
        DebitCardRefund = 6,
        CreditCardOffline = 7,
        CreditCardOfflineRefund = 8,
        CheckSales = 9,
        CheckRefund = 10,
        GiftCard = 11,
        GiftCardRefund = 12,
        CreditCard1 = 13,
        CreditCardRefund1 = 14,
        Check = 15,
        DebitCard1 = 16,
        Zelle = 17,
        Venmo = 18,
        Paypal = 19
    }
}
