using System.Text.Json;
using Discord.Protocol.Request;
using Discord.Protocol.Tests.Examples;
using Xunit;

namespace Discord.Protocol.Tests
{
    public class RequestConverterTests
    {
        [Fact]
        public void ConvertFlatRequest()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"string\",\"type\":3,\"value\":\"value\"}," +
                "{\"name\":\"int\",\"type\":4,\"value\":314}," +
                "{\"name\":\"bool\",\"type\":5,\"value\":true}," +
                "{\"name\":\"number\",\"type\":10,\"value\":3.14}," +
                "{\"name\":\"optstring\",\"type\":3,\"value\":\"value\"}," +
                "{\"name\":\"optint\",\"type\":4,\"value\":314}," +
                "{\"name\":\"optbool\",\"type\":5,\"value\":true}," +
                "{\"name\":\"optnumber\",\"type\":10,\"value\":3.14}" +
                "],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            ExampleCommandRequest commandRequest = interactionRequest.ConvertRequest<ExampleCommandRequest>();

            Assert.Equal("value", commandRequest.String);
            Assert.Equal(314, commandRequest.Int);
            Assert.True(commandRequest.Bool);
            Assert.Equal(3.14, commandRequest.Number);
            Assert.Equal("value", commandRequest.OptString);
            Assert.Equal(314, commandRequest.OptInt);
            Assert.True(commandRequest.OptBool);
            Assert.Equal(3.14, commandRequest.OptNumber);
        }
        
        [Fact]
        public void ConvertFlatRequestWithoutOptionalValue()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"string\",\"type\":3,\"value\":\"value\"}," +
                "{\"name\":\"int\",\"type\":4,\"value\":314}," +
                "{\"name\":\"bool\",\"type\":5,\"value\":true}," +
                "{\"name\":\"number\",\"type\":10,\"value\":3.14}" +
                "],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            ExampleCommandRequest commandRequest = interactionRequest.ConvertRequest<ExampleCommandRequest>();

            Assert.Equal("value", commandRequest.String);
            Assert.Equal(314, commandRequest.Int);
            Assert.True(commandRequest.Bool);
            Assert.Equal(3.14, commandRequest.Number);
            Assert.Null(commandRequest.OptString);
            Assert.Null( commandRequest.OptInt);
            Assert.Null(commandRequest.OptBool);
            Assert.Null(commandRequest.OptNumber);
        }

        [Fact]
        public void ConvertSubCommandRequestA()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"subcommanda\",\"type\":1,\"options\":[" +
                "{\"name\":\"requireddouble\",\"type\":10,\"value\":3.14}," +
                "{\"name\":\"optionalint\",\"type\":4,\"value\":314}" +
                "]}],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            ExampleSubCommandRequest commandRequest = interactionRequest.ConvertRequest<ExampleSubCommandRequest>();

            Assert.Equal(3.14, commandRequest.SubCommandA.RequiredDouble);
            Assert.Equal(314, commandRequest.SubCommandA.OptionalInt);
            Assert.Null(commandRequest.SubCommandB);
        }
        
        [Fact]
        public void ConvertSubCommandRequestAWithoutOptionalValue()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"subcommanda\",\"type\":1,\"options\":[" +
                "{\"name\":\"requireddouble\",\"type\":10,\"value\":3.14}" +
                "]}],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            ExampleSubCommandRequest commandRequest = interactionRequest.ConvertRequest<ExampleSubCommandRequest>();

            Assert.Equal(3.14, commandRequest.SubCommandA.RequiredDouble);
            Assert.Null(commandRequest.SubCommandA.OptionalInt);
            Assert.Null(commandRequest.SubCommandB);
        }

        [Fact]
        public void ConvertSubCommandRequestOnlyOptionalProperties()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"subcommandopt\",\"type\":1" +
                "}],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            ExampleSubCommandRequest commandRequest = interactionRequest.ConvertRequest<ExampleSubCommandRequest>();

            Assert.NotNull(commandRequest.SubCommandOpt);
            Assert.Null(commandRequest.SubCommandA);
            Assert.Null(commandRequest.SubCommandB);
        }

        [Fact]
        public void ConvertSubCommandGroupRequest()
        {
            string requestBody =
                "{\"data\":{\"id\":\"12345\",\"name\":\"test\",\"options\":[" +
                "{\"name\":\"subcommandgroup1\",\"type\":2,\"options\":[" +
                "{\"name\":\"subsubcommand1a\",\"type\":1,\"options\":[" +
                "{\"name\":\"requireddouble\",\"type\":10,\"value\":3.14}," +
                "{\"name\":\"optionalint\",\"type\":4,\"value\":314}" +
                "]}]}],\"type\":1}}";

            InteractionRequest interactionRequest = JsonSerializer.Deserialize<InteractionRequest>(requestBody);

            ExampleSubCommandGroupRequest commandRequest = interactionRequest.ConvertRequest<ExampleSubCommandGroupRequest>();

            Assert.Equal(3.14, commandRequest.SubCommandGroup1.SubSubCommand1A.RequiredDouble);
            Assert.Equal(314, commandRequest.SubCommandGroup1.SubSubCommand1A.OptionalInt);
            Assert.Null(commandRequest.SubCommandGroup2);
        }
    }
}