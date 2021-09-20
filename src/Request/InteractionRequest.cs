using System.Text.Json.Serialization;
using Discord.Protocol.Data;

namespace Discord.Protocol.Request
{
    public class InteractionRequest
    {
        [JsonPropertyName("application_id")]
        public string ApplicationId { get; set; }

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }

        [JsonPropertyName("data")]
        public Data.Data Data { get; set; }

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("member")]
        public Member Member { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("type")]
        public InteractionType Type { get; set; }

        [JsonPropertyName("version")]
        public long Version { get; set; }
    }
}