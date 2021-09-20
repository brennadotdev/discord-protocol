using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Discord.Protocol.Data
{
    public class Member
    {
        [JsonPropertyName("deaf")]
        public bool Deaf { get; set; }

        [JsonPropertyName("is_pending")]
        public bool IsPending { get; set; }

        [JsonPropertyName("joined_at")]
        public DateTimeOffset JoinedAt { get; set; }

        [JsonPropertyName("mute")]
        public bool Mute { get; set; }

        [JsonPropertyName("nick")]
        public string Nick { get; set; }

        [JsonPropertyName("pending")]
        public bool Pending { get; set; }

        [JsonPropertyName("permissions")]
        public string Permissions { get; set; }

        [JsonPropertyName("roles")]
        public List<object> Roles { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }
    }
}