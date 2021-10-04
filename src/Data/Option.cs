using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Discord.Protocol.Data
{
    public class Option
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("type")]
        public ApplicationCommandOptionType Type { get; set; }
        
        [JsonPropertyName("value")]
        public object Value { get; set; }
        
        [JsonPropertyName("options")]
        public List<Option> Options { get; set; }
    }
}