using Newtonsoft.Json;

namespace InvoiceBulkRegisteration.Dtos
{
    public class SampleCustomerDto
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("phone")]
        public string Phone {  get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
    }
}
