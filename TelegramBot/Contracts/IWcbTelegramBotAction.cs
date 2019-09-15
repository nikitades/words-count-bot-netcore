using Telegram.Bot.Types.Enums;

namespace WordsCountBot.Contracts
{
    public interface IWcbTelegramBotAction
    {
        public WcbActionType Type { get; set; }
        public long ChatID { get; set; }
        public string Text { get; set; }
        public ParseMode Mode { get; set; }
    }
}