using Telegram.Bot.Types;

namespace WordsCountBot.Contracts
{
    public interface ITelegramBot
    {
        public void HandleUpdate(Update update);
    }
}