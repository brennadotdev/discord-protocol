using System.Text.Json.Serialization;
using Discord.Protocol.Data;

namespace Discord.Protocol.Response
{
    public class InteractionResponse
    {
        [JsonPropertyName("type")]
        public InteractionType Type { get; set; }
        
        [JsonPropertyName("data")]
        public Data.Data Data { get; set; }
    }
}