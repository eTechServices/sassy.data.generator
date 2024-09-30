using Newtonsoft.Json;
using System;

namespace sassy.bulk.ResponseDto
{
    public class RegisterResponseDto
    {
        [JsonProperty("locationId")]
        public int LocationId { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [JsonProperty("registerId")]

        public long RegisterId { get; set; }

        [JsonProperty("registerName")]
        public string RegisterName { get; set; }

        [JsonProperty("addedBy")]
        public string AddedBy { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("cashierMachine")]
        public bool CashierMachine { get; set; }

        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("isCurrentRegister")]
        public bool IsCurrentRegister { get; set; }

        [JsonProperty("isEnableSecondaryScreen")]
        public bool IsEnableSecondaryScreen { get; set; }

        [JsonProperty("useCashDrawer")]
        public bool UseCashDrawer { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }
    }
}
