using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WordsCountBot.Contracts;

namespace WordsCountBot.TelegramBot
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

        public Task<Message> SendTextMessageAsync(long chatId, string text, ParseMode mode)
        {
            return _client.SendTextMessageAsync(chatId, text, mode);
        }

        public Task SetWebhookAsync()
        {
            return _client.SetWebhookAsync(_options.Value.WebhookUrl);
        }
    }
}