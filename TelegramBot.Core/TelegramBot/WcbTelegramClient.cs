using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Core.Contracts;

namespace TelegramBot.Core.TelegramBot
{
    public class WcbTelegramClient : IWcbTelegramClient
    {

        private IOptions<WcbTelegramBotConfig> _options;
        private TelegramBotClient _client;

        public WcbTelegramClient(
            IOptions<WcbTelegramBotConfig> options
        )
        {
            _options = options;
            _client = new TelegramBotClient(_options.Value.BotToken);
        }

        public WcbTelegramClient(string token)
        {
            _client = new TelegramBotClient(token);
        }

        public Task<Message> SendTextMessageAsync(long chatId, string text, ParseMode mode)
        {
            return _client.SendTextMessageAsync(chatId, text, mode);
        }

        public Task SetWebhookAsync()
        {
            return _client.SetWebhookAsync(_options.Value.WebhookUrl);
        }

        public async Task<string> GetWebhookAsync()
        {
            var cts = new CancellationTokenSource();
            var info = await _client.GetWebhookInfoAsync(cts.Token);
            return info.Url;
        }


        public Task SetWebhookAsync(string webhookUrl)
        {
            return _client.SetWebhookAsync(webhookUrl);
        }
    }
}