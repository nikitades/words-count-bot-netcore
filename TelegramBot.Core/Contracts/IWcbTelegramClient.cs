using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Core.Contracts
{
    public interface IWcbTelegramClient
    {
        Task SetWebhookAsync();
        Task SetWebhookAsync(string webhookUrl);
        Task<string> GetWebhookAsync();
        Task<Message> SendTextMessageAsync(long chatId, string text, ParseMode mode);
    }
}