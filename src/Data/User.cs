using System.Text.Json.Serialization;

namespace Discord.Protocol.Data
{
    public class User
    {
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        [JsonPropertyName("discriminator")]
        public string Discriminator { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("public_flags")]
        public long PublicFlags { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}