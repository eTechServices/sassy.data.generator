using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace sassy.bulk.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RecordType
    {
        None,
        CreditDebitReceipt,
        InvoiceReceipt,
        PayoutReceipt,
        CashDropReceipt,
        BackOfficeInvoiceReceipt
    }
}
