using System.Threading.Tasks;

namespace TelegramBot.Cli.Commands
{
    interface ICommand
    {
        Task Execute();
    }
}