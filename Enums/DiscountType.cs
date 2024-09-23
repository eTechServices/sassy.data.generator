using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DiscountType
    {
        FlatOnSpecificItem,
        PercentageOnSpecificItem,
        FlatOnWholeSale,
        PercentageOnWholeSale,
        PercentageCoupon,
        FlatCoupon,
        RewardDiscount,
    }
}
