using System;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WordsCountBot.Contracts;

namespace WordsCountBot.TelegramBot
{
    public class TelegramBot : ITelegramBot
    {
        private TelegramBotClient _client;

        public TelegramBot(IOptions<TelegramBotConfig> options)
        {
            _client = new TelegramBotClient(options.Value.BotToken);
            _client.SetWebhookAsync(options.Value.WebhookUrl);
        }

        public void HandleUpdate(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    handleGenericMessage(update);
                    break;
            }
        }

        private void handleGenericMessage(Update update)
        {
            if (!Array.Exists(new[]{
                ChatType.Group,
                ChatType.Supergroup
            }, type => type == update.Message.Chat.Type)) {
                Console.WriteLine("Not from chat"); 
                return;
            };
            Console.WriteLine("From chat!");
        }
    }
}