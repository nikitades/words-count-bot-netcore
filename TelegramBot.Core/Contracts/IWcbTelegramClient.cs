using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Core.Contracts
{
    public interface IWcbTelegramClient
    {
        public Task SetWebhookAsync();
        public Task SetWebhookAsync(string webhookUrl);
        public Task<string> GetWebhookAsync();
        public Task<Message> SendTextMessageAsync(long chatId, string text, ParseMode mode);
    }
}