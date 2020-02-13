using System.Threading.Tasks;

namespace TelegramBot.Cli.Commands
{
    public interface ICommand
    {
        Task<ICommandResult> Execute();
    }
}