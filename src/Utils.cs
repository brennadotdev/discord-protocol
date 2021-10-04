using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.Protocol.Data;
using Discord.Protocol.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sodium;

namespace Discord.Protocol
{
    public static class Utils
    {
        public static bool IsSignatureValid(IDictionary<string, string> headers, string body, byte[] byteKey)
        {
            try
            {
                string sig = headers["x-signature-ed25519"];
                byte[] byteSig = ConvertStringToByteKey(sig);
                string timestamp = headers["x-signature-timestamp"];
                byte[] byteBody = Encoding.Default.GetBytes(timestamp + body);
                return PublicKeyAuth.VerifyDetached(byteSig, byteBody, byteKey);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static readonly int[] HexValue =
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
            0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
        };

        // Code Snippet from: https://stackoverflow.com/a/5919521/11682098
        // Code Snippet by: Nathan Moinvaziri
        public static byte[] ConvertStringToByteKey(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];

            for (int x = 0, i = 0; i < hex.Length; i += 2, x += 1)
            {
                bytes[x] = (byte) (HexValue[char.ToUpper(hex[i + 0]) - '0'] << 4 |
                                   HexValue[char.ToUpper(hex[i + 1]) - '0']);
            }

            return bytes;
        }

        public static string GetMemberAddressAsMention(this Member member)
        {
            return $"<@{member.User.Id}>";
        }

        public static T ConvertRequest<T>(this InteractionRequest request) where T : new()
        {
            if (request?.Data?.Options == null || !request.Data.Options.Any())
                return new T();

            JObject resultObject = ParseOptions(request.Data.Options);

            return JsonConvert.DeserializeObject<T>(resultObject.ToString(Formatting.None));

            JObject ParseOptions(List<Option> options)
            {
                JObject jObject = new JObject();

                foreach (Option o in options)
                {
                    switch (o.Type)
                    {
                        case ApplicationCommandOptionType.SubCommand:
                        case ApplicationCommandOptionType.SubCommandGroup:
                            if (o.Options == null)
                                jObject[o.Name] = new JObject();
                            else
                                jObject[o.Name] = ParseOptions(o.Options);
                            break;
                        case ApplicationCommandOptionType.String:
                        case ApplicationCommandOptionType.Integer:
                        case ApplicationCommandOptionType.Boolean:
                        case ApplicationCommandOptionType.Number:
                            jObject[o.Name] = new JValue(o.Value.ToString());
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                return jObject;
            }
        }
    }
}