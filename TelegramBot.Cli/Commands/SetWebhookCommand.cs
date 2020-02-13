using System;
using System.Linq;
using System.Threading.Tasks;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.TelegramBot;

namespace TelegramBot.Cli.Commands
{
    class SetWebhookCommand : ICommand
    {
        private IWcbTelegramClient _client;
        private string _webhook;
        private Options _opts;

        public SetWebhookCommand(Options opts)
        {
            _opts = opts;
            if (String.IsNullOrWhiteSpace(_opts.Token)) throw new ArgumentNullException("No bot token provided!");
            _client = new WcbTelegramClient(_opts.Token);
            _webhook = _opts.SecondValue;
        }

        public async Task Execute()
        {
            await _client.SetWebhookAsync(_webhook);
            Console.WriteLine($"Webhook successfully set to: {_webhook}");
        }
    }
}