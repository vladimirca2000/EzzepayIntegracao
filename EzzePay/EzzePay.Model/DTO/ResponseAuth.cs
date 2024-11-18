using System.Text.Json.Serialization;

namespace EzzePay.Model.DTO
{
    public class ResponseAuth
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        public DateTime TokenExpiration { get; set; }
    }
}
