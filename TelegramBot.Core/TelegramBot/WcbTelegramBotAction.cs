using Telegram.Bot.Types.Enums;
using TelegramBot.Core.Contracts;

namespace TelegramBot.Core.TelegramBot
{
    public class WcbTelegramBotAction : IWcbTelegramBotAction
    {
        public string Text { get; set; }
        public long ChatID { get; set; }
        public ParseMode Mode { get; set; }
        public WcbActionType Type { get; set; }
    }
}