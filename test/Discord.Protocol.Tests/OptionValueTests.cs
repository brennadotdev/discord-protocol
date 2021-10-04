using System.Text.Json;
using Discord.Protocol.Request;
using Xunit;

namespace Discord.Protocol.Tests
{
    public class OptionValueTests
    {
        [Fact]
        public void BasicValuesTest()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"string\",\"type\":3,\"value\":\"stringValue\"}," +
                "{\"name\":\"int\",\"type\":4,\"value\":1}," +
                "{\"name\":\"boolean\",\"type\":5,\"value\":true}," +
                "{\"name\":\"number\",\"type\":10,\"value\":3.14}" +
                "],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            string actualStringValue = interactionRequest.GetOptionValue<string>("string");
            Assert.Equal("stringValue", actualStringValue);

            int actualIntValue = interactionRequest.GetOptionValue<int>("int");
            Assert.Equal(1, actualIntValue);

            bool actualBoolValue = interactionRequest.GetOptionValue<bool>("boolean");
            Assert.True(actualBoolValue);

            double actualNumberValue = interactionRequest.GetOptionValue<double>("number");
            Assert.Equal(3.14, actualNumberValue);
        }

        [Fact]
        public void OptionalValuesTest()
        {
            string requestBody = "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            string actualStringValue = interactionRequest.GetOptionValue<string>("string");
            Assert.Null(actualStringValue);

            int? actualIntValue = interactionRequest.GetOptionValue<int?>("int");
            Assert.Null(actualIntValue);

            bool? actualBoolValue = interactionRequest.GetOptionValue<bool?>("boolean");
            Assert.Null(actualBoolValue);

            double? actualNumberValue = interactionRequest.GetOptionValue<double?>("number");
            Assert.Null(actualNumberValue);
        }

        [Fact]
        public void OptionalAndRequiredValuesTest()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"string\",\"type\":3,\"value\":\"stringValue\"}" +
                "],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            string actualStringValue = interactionRequest.GetOptionValue<string>("string");
            Assert.Equal("stringValue", actualStringValue);

            int? actualIntValue = interactionRequest.GetOptionValue<int?>("int");
            Assert.Null(actualIntValue);
        }
    }
}