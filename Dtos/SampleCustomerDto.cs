using Newtonsoft.Json;
using System;

namespace InvoiceBulkRegisteration.Dtos
{
    public class SampleCustomerDto
    {
        public SampleCustomerDto()
        {
            Id = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        }

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
