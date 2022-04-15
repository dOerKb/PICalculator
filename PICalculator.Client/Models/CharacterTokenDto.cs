using Newtonsoft.Json;

namespace PICalculator.Client.Models
{
    internal class CharacterTokenDto
    {
        [JsonProperty("CharacterID")]
        internal int CharacterID { get; set; }

        [JsonProperty("CharacterName")]
        internal string CharacterName { get; set; }

        [JsonProperty("ExpiresOn")]
        internal string ExpiresOn { get; set; }

        [JsonProperty("Scopes")]
        internal string Scopes { get; set; }

        [JsonProperty("TokenType")]
        internal string TokenType { get; set; }

        [JsonProperty("CharacterOwnerHash")]
        internal string CharacterOwnerHash { get; set; }

        [JsonProperty("IntellectualProperty")]
        internal string IntellectualProperty { get; set; }
    }
}
