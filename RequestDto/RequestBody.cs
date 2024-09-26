using Newtonsoft.Json;
using sassy.bulk.Enums;
using System.Collections.Generic;

namespace sassy.bulk.RequestDto
{
    public class RequestBody
    {
        [JsonProperty("endDate")]
        public string EndDate { get; set; } = "09/19/2024";
        [JsonProperty("filterValue")]
        public string FilterValue { get; set; } = "";
        [JsonProperty("invoiceType")]
        public TransactionType InvoiceType { get; set; }
        [JsonProperty("locationIds")]
        public List<string> LocationsIds { get; set; } = new List<string>() { "1670001", "1670002", "1670011", "1670012", "1670013", "1670014" };
        [JsonProperty("orderBy")]
        public string OrderBy { get; set; } = "";
        [JsonProperty("orderParam")]
        public string OrderParam { get; set; } = "";
        [JsonProperty("orderPlacement")]
        public string OrderPlacement { get; set; } = "All";
        [JsonProperty("pageNo")]
        public int PageNo { get; set; } = 0;
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;
        [JsonProperty("postedStatus")]
        public PostedStatus PostedStatus { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; } = "09/19/2024";
    }
}
