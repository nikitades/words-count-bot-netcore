using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using WordsCountBot.Contracts;

namespace WordsCountBot.TelegramBot
{
    public class WcbTelegramClient : TelegramBotClient
    {

        private IOptions<WcbTelegramBotConfig> _options;
        private TelegramBotClient _client;
        public WcbTelegramClient(
            IOptions<WcbTelegramBotConfig> options
        ) : base(options.Value.BotToken)
        {
            _options = options;
            _client = new TelegramBotClient(_options.Value.BotToken);
        }

        public Task SetWebhookAsync()
        {
            return base.SetWebhookAsync(_options.Value.WebhookUrl);
        }
    }
}