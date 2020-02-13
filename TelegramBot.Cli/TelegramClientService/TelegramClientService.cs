using TelegramBot.Core.Contracts;
using TelegramBot.Core.TelegramBot;

namespace TelegramBot.Cli.TelegramClientService
{
    public interface ITelegramClientService
    {
        IWcbTelegramClient GetClient(Options opts);
    }

    public class Service : ITelegramClientService
    {
        public IWcbTelegramClient GetClient(Options opts)
        {
            return new WcbTelegramClient(opts.Token);
        }
    }
}