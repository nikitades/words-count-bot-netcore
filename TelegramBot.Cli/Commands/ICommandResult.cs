namespace TelegramBot.Cli.Commands
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}