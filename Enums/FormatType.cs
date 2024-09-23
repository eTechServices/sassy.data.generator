using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FormatType
    {
        Image,
        Html,
        Pdf,
        Docs
    }
}
