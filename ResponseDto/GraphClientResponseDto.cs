using Newtonsoft.Json;

namespace sassy.bulk.ResponseDto
{
    public class GraphClientResponseDto
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        [JsonProperty("companyName")]
        public string CompanyName { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
    }
}
