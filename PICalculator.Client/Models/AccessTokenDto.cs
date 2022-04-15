using Newtonsoft.Json;

namespace PICalculator.Client.Models
{
    internal class AccessTokenDto
    {
        [JsonProperty("access_token")]
        internal string AccessToken { get; set; }

        [JsonProperty("token_type")]
        internal string TokenType { get; set; }

        [JsonProperty("expires_in")]
        internal int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        internal string RefreshToken { get; set; }
    }
}
