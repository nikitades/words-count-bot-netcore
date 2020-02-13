using System.Windows.Input;
using System;
using System.Threading.Tasks;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.TelegramBot;

namespace TelegramBot.Cli.Commands
{
    public class GetWebhookCommand : ICommand
    {
        private IWcbTelegramClient _client;
        private Options _opts;

        public GetWebhookCommand(Options opts)
        {
            _opts = opts;
            if (String.IsNullOrWhiteSpace(_opts.Token)) throw new ArgumentNullException("No bot token provided!");
            _client = new WcbTelegramClient(_opts.Token);
        }

        public async Task Execute()
        {
            var wh = await _client.GetWebhookAsync();
            Console.WriteLine($"Current webhook: {wh}");
        }
    }
}