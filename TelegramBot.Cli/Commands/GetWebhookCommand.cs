using System.Windows.Input;
using System;
using System.Threading.Tasks;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.TelegramBot;
using TelegramBot.Cli.TelegramClientService;

namespace TelegramBot.Cli.Commands
{
    public class GetWebhookCommand : ICommand
    {
        private IWcbTelegramClient _client;
        private Options _opts;

        public GetWebhookCommand(Options opts, ITelegramClientService tgClientService)
        {
            _opts = opts;
            if (String.IsNullOrWhiteSpace(_opts.Token)) throw new ArgumentNullException("No bot token provided!");
            _client = tgClientService.GetClient(_opts);
        }

        public async Task<ICommandResult> Execute()
        {
            try
            {
                var wh = await _client.GetWebhookAsync();
                return new CommandResult
                {
                    Success = true,
                    Message = $"Current webhook: {wh}"
                };
            }
            catch (Exception e)
            {
                return new CommandResult
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }
    }
}