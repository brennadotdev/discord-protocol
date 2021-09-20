using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Discord.Protocol.Data
{
    public class Data
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public long Type { get; set; }
        
        [JsonPropertyName("content")]
        public string Content { get; set; }
        
        [JsonPropertyName("options")]
        public List<Option> Options { get; set; }
    }
}