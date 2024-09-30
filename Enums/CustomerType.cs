using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerType
    {
        All = 0,
        LoyaltyCustomer = 1,
        CashCustomer = 2,
        Group = 3,
        NewCustomer = 4,
        ExistingCustomer = 5
    }
}
