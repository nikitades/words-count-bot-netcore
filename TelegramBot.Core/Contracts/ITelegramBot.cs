using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Core.Contracts
{
    public interface ITelegramBot
    {
        IWcbTelegramBotAction HandleUpdate(Update update);
        Task<Message> ProcessAction(IWcbTelegramBotAction action);
        void SetWebhookAsync();
    }
}