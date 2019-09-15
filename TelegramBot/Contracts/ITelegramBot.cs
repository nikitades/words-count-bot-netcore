using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace WordsCountBot.Contracts
{
    public interface ITelegramBot
    {
        public IWcbTelegramBotAction HandleUpdate(Update update);
        public Task<Message> ProcessAction(IWcbTelegramBotAction action);
        public void SetWebhook();
    }
}