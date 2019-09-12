using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WcbTelegramBot : ITelegramBot
    {
        private WcbTelegramClient _client;
        private ILogger<WcbTelegramBot> _logger;
        private IWordsRepository<Word, WordsCountBotDbContext> _wordsRepo;
        private IChatsRepository<MyChat, WordsCountBotDbContext> _chatsRepo;
        private IUsagesRepository<WordUsedTimes, WordsCountBotDbContext> _usagesRepo;

        public WcbTelegramBot(
            ILogger<WcbTelegramBot> logger,
            IWordsRepository<Word, WordsCountBotDbContext> wordsRepo,
            IChatsRepository<MyChat, WordsCountBotDbContext> chatsRepo,
            IUsagesRepository<WordUsedTimes, WordsCountBotDbContext> usagesRepo,
            WcbTelegramClient client
        )
        {
            _logger = logger;
            _wordsRepo = wordsRepo;
            _chatsRepo = chatsRepo;
            _usagesRepo = usagesRepo;
            _client = client;
        }

        public void SetWebhook()
        {
            _client.SetWebhookAsync();
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
            var words = Word.GetWordsFromText(text).Where(word => !word.TooShort).ToList();

            _chatsRepo.Create(chat);
            _chatsRepo.GetContext().SaveChanges();
            _wordsRepo.Create(words);
            _wordsRepo.GetContext().SaveChanges();
            _usagesRepo.IncrementLinks(words, chat);
        }

        private void handleCountMessage(Update update)
        {
            var text = update.Message.Text.Substring(("/count".Length)).Trim();
            text = Word.EscapeString(text);

            IEnumerable<Word> words;
            IEnumerable<WordUsedTimes> usages;

            if (String.IsNullOrEmpty(text))
            {
                usages = _usagesRepo.GetByTelegramIdTopWords(update.Message.Chat.Id, 3);
                words = _wordsRepo.GetByID(usages.Select(usage => usage.WordID));

                _client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    String.Join("\n", words.Select(word => $"<b>{word.Text}</b>: {usages.Where(usage => usage.WordID == word.ID).First().UsedTimes}")),
                    ParseMode.Html
                );
            }
            else
            {
                IEnumerable<Word> sourceWords = Word.GetWordsFromText(text).Take(3);
                words = _wordsRepo.GetByText(sourceWords.Select(word => word.Text).ToList());
                usages = _usagesRepo.GetByTelegramIdAndWordsList(update.Message.Chat.Id, words.Select(word => word.Text));

                var responseText = new List<string>();
                foreach (var sourceWord in sourceWords)
                {
                    if (sourceWord.TooShort)
                    {
                        responseText.Add($"<b>{sourceWord.Text}</b>: too short");
                        continue;
                    }
                    var foundWord = words.Where(word => word.Text == sourceWord.Text).FirstOrDefault();
                    if (foundWord == null)
                    {
                        responseText.Add($"<b>{sourceWord.Text}</b>: not found");
                        continue;
                    }
                    var foundWordUsage = usages.Where(usage => usage.WordID == foundWord.ID).First();
                    responseText.Add($"<b>{sourceWord.Text}</b>: {foundWordUsage.UsedTimes}");
                }

                _client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    String.Join("\n", responseText),
                    ParseMode.Html
                );
            }


        }
    }
}