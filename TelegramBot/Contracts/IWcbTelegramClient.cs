using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WordsCountBot.Contracts
{
    public interface IWcbTelegramClient
    {
        public Task SetWebhookAsync();
        public Task<Message> SendTextMessageAsync(long chatId, string text, ParseMode mode);
    }
}