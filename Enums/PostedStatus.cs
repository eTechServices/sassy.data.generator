using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PostedStatus
    {
        Draft = 1,
        Completed = 2,
        Hold = 3,
        Processing = 4,
        PendingPayment = 5,
        Cancelled = 6,
        Refunded = 7,
        Failed = 8
    }
}
