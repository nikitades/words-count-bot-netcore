namespace TelegramBot.Cli.Commands
{
    public class CommandResult : ICommandResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}