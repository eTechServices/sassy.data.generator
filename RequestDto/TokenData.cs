using Newtonsoft.Json;
using sassy.bulk.Enums;

namespace sassy.bulk.RequestDto
{
    public class TokenData
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpireIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("id_token")]
        public string TokenId { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("accountStatus")]
        public AccountStatus AccountStatus { get; set; }
        [JsonProperty("accountStatusString")]
        public string AccountStatusString { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
