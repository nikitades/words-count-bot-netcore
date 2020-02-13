using Telegram.Bot.Types.Enums;

namespace TelegramBot.Core.Contracts
{
    public interface IWcbTelegramBotAction
    {
        WcbActionType Type { get; set; }
        long ChatID { get; set; }
        string Text { get; set; }
        ParseMode Mode { get; set; }
    }
}