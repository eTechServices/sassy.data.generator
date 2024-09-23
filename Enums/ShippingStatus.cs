using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ShippingStatus
    {
        Delivered = 1,
        PendingDelivery = 2,
        Dispatched = 3
    }
}
