using System;
using System.Linq;
using System.Threading.Tasks;
using TelegramBot.Cli.TelegramClientService;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.TelegramBot;

namespace TelegramBot.Cli.Commands
{
    public class SetWebhookCommand : ICommand
    {
        private IWcbTelegramClient _client;
        private string _webhook;
        private Options _opts;

        public SetWebhookCommand(Options opts, ITelegramClientService tgClientService)
        {
            _opts = opts;
            if (String.IsNullOrWhiteSpace(_opts.Token)) throw new ArgumentNullException("No bot token provided!");
            _client = tgClientService.GetClient(_opts);
            _webhook = _opts.SecondValue;
        }

        public async Task<ICommandResult> Execute()
        {
            try
            {
                await _client.SetWebhookAsync(_webhook);
            }
            catch (Exception e)
            {
                return new CommandResult
                {
                    Success = false,
                    Message = e.Message
                };
            }
            return new CommandResult
            {
                Success = true,
                Message = $"Webhook successfully set to: {_webhook}"
            };
        }
    }
}