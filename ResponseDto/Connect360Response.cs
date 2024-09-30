using Newtonsoft.Json;
using System.Collections.Generic;

namespace sassy.bulk.ResponseDto
{
    public class Connect360Response
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("responseStatusCode")]
        public string ResponseStatusCode { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("totalRecords")]
        public int TotalRecords { get; set; } = 0;
        [JsonProperty("elapsedTime")]
        public string ElapsedTime { get; set; }
        [JsonProperty("showThisMessage")]
        public bool ShowThisMessage { get; set; }
    }
    public class ObjectResponse
    {
        [JsonProperty("locations")]
        public List<LocationsResponseDto> Locations { get; set; }
        [JsonProperty("registers")]
        public List<RegisterResponseDto> Registers { get; set; }
    }
}
