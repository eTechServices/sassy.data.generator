using Newtonsoft.Json;

namespace sassy.bulk.RequestDto
{
    public class UserDataRequestDto
    {
        [JsonProperty("Name")]
        public string Name {  get; set; }
        [JsonProperty("User")]
        public UserParameter Parameter { get; set; }
    }
    public class UserParameter
    {
        [JsonProperty("Username")]
        public string UserName { get; set; }
    }
}
