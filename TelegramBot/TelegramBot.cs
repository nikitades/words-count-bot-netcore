using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WordsCountBot.Contracts;
using WordsCountBot.Database;
using WordsCountBot.Models;
using MyChat = WordsCountBot.Models.Chat;

namespace WordsCountBot.TelegramBot
{
    public class TelegramBot : ITelegramBot
    {
        private TelegramBotClient _client;
        private ILogger<TelegramBot> _logger;
        private IWordsRepository<Word, WordsCountBotDbContext> _wordsRepo;
        private IChatsRepository<MyChat, WordsCountBotDbContext> _chatsRepo;
        private IUsagesRepository<WordUsedTimes, WordsCountBotDbContext> _usagesRepo;
        private IOptions<TelegramBotConfig> _options;

        public TelegramBot(
            IOptions<TelegramBotConfig> options,
            ILogger<TelegramBot> logger,
            IWordsRepository<Word, WordsCountBotDbContext> wordsRepo,
            IChatsRepository<MyChat, WordsCountBotDbContext> chatsRepo,
            IUsagesRepository<WordUsedTimes, WordsCountBotDbContext> usagesRepo
        )
        {
            _logger = logger;
            _wordsRepo = wordsRepo;
            _chatsRepo = chatsRepo;
            _usagesRepo = usagesRepo;
            _options = options;
            _client = new TelegramBotClient(_options.Value.BotToken);
        }

        public void SetWebhook()
        {
            _client.SetWebhookAsync(_options.Value.WebhookUrl);
        }

        public void HandleUpdate(Update update)
        {
            if (update.Message == null || update.Message.Text == null)
            {
                _logger.LogDebug($"Received an empty message");
                return;
            }

            if (update.Type != UpdateType.Message)
            {
                _logger.LogDebug($"Ignoring the message of type {update.Type} from chat {update.Message.Chat.Id} ({update.Message.Chat.Title})");
                return;
            }

            if (!Array.Exists(new[]{
                ChatType.Group,
                ChatType.Supergroup
            }, type => type == update.Message.Chat.Type))
            {
                _logger.LogDebug($"Ingoring message from wrong chat type ({update.Message.Chat.Type}) from chat {update.Message.Chat.Id} ({update.Message.Chat.Title})");
                return;
            };
            _logger.LogDebug($"New message from chat {update.Message.Chat.Title} ({update.Message.Chat.Title})");

            if (update.Message.Text.StartsWith("/count"))
            {
                handleCountMessage(update);
            }
            else
            {
                handleGenericMessage(update);
            }
        }

        private void handleGenericMessage(Update update)
        {
            var chat = new MyChat
            {
                Name = update.Message.Chat.Title,
                TelegramID = update.Message.Chat.Id
            };
            var text = Word.EscapeString(update.Message.Text);
            var words = Word.GetWordsFromText(text);

            _chatsRepo.Create(chat);
            // _chatsRepo.GetContext().SaveChanges();
            _wordsRepo.Create(words);
            _wordsRepo.GetContext().SaveChanges();
            _usagesRepo.IncrementLinks(words, chat);
            _usagesRepo.GetContext().SaveChanges();
        }

        private void handleCountMessage(Update update)
        {

        }
    }
}