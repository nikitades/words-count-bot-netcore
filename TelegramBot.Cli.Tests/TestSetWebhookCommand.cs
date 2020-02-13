using System;
using System.Threading.Tasks;
using Xunit;
using TelegramBot.Cli.TelegramClientService;
using Moq;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.TelegramBot;
using Xunit.Abstractions;

namespace TelegramBot.Cli.Tests
{
    public class TestSetWebhookCommand
    {
        public static string FakeToken = "fakeToken";
        public static string TestWebhookUrl = "TravaTrava";

        [Fact]
        public async Task TestSuccessAsync()
        {
            var fakeTelegramClient = new Mock<IWcbTelegramClient>();
            fakeTelegramClient
                .Setup(client => client.SetWebhookAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var fakeTelegramClientService = new Mock<ITelegramClientService>();
            fakeTelegramClientService
                .Setup(repo => repo.GetClient(It.IsAny<Options>()))
                .Returns(fakeTelegramClient.Object);

            var fakeOpts = new Options
            {
                Token = FakeToken,
                FirstValue = "SetWebhook",
                SecondValue = TestWebhookUrl
            };

            var mode = new Mode(fakeOpts, fakeTelegramClientService.Object);
            var res = await mode.Command.Execute();
            Assert.Equal(res.Message, $"Webhook successfully set to: {TestWebhookUrl}");
        }
    }
}
