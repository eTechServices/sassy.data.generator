using Newtonsoft.Json;
using System;

namespace sassy.bulk.ResponseDto
{
    public class LocationsResponseDto
    {
        [JsonProperty("locationId")]
        public int LocationId { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [JsonProperty("manageInventory")]

        public bool ManageInventory { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]

        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("country")]
        public string Country { get; set;}

        [JsonProperty("isHeadquater")]
        public bool IsHeadquater { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("allowShipping")]

        public bool AllowShipping { get; set; }

        [JsonProperty("acceptOnlineOrders")]
        public bool AcceptOnlineOrders { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
    }
}
