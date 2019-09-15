using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WordsCountBot.Contracts;
using WordsCountBot.Database;
using WordsCountBot.Models;
using WordsCountBot.Repositories;
using WordsCountBot.TelegramBot;
using Xunit;

namespace TestTelegramBot
{
    public class TestTelegramBot
    {
        Word _wordWithNoUsagesInCurrentChat = new Word
        {
            Text = "kek",
            ID = 221
        };

        Word _wordWithTwoUsagesInCurrentChat = new Word
        {
            Text = "kekTwoUsages",
            ID = 222
        };

        Word _tooShortWord = new Word
        {
            Text = "le",
            ID = 220
        };

        WordsCountBot.Models.Chat _currentChat = new WordsCountBot.Models.Chat
        {
            ID = 333,
            TelegramID = -3456
        };

        private WcbTelegramBot createMockedClient(
            Mock<ILogger<WcbTelegramBot>> logger = null,
            Mock<IWordsRepository<Word, WordsCountBotDbContext>> wordsRepo = null,
            Mock<IChatsRepository<WordsCountBot.Models.Chat, WordsCountBotDbContext>> chatsRepo = null,
            Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>> usagesRepo = null,
            Mock<IWcbTelegramClient> client = null
        )
        {
            var _logger = logger ?? new Mock<ILogger<WcbTelegramBot>>();
            var _wordsRepo = wordsRepo ?? new Mock<IWordsRepository<Word, WordsCountBotDbContext>>();
            var _chatsRepo = chatsRepo ?? new Mock<IChatsRepository<WordsCountBot.Models.Chat, WordsCountBotDbContext>>();
            var _usagesRepo = usagesRepo ?? new Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>>();
            var _client = client ?? new Mock<IWcbTelegramClient>();
            var tgBot = new WcbTelegramBot(
                _logger.Object,
                _wordsRepo.Object,
                _chatsRepo.Object,
                _usagesRepo.Object,
                _client.Object
            );
            return tgBot;
        }

        private Update createUpdate(long chatID, string text, ChatType chatType)
        {
            return new Update
            {
                Message = new Message
                {
                    Text = text,
                    Chat = new Telegram.Bot.Types.Chat
                    {
                        Type = chatType,
                        Id = chatID
                    }
                },
            };
        }

        [Fact]
        public void TestCountCommandWithTwoUsagesFound()
        {
            var fakeWordsRepo = new Mock<IWordsRepository<Word, WordsCountBotDbContext>>();
            fakeWordsRepo.Setup(repo => repo.GetByText(new List<string>() {
                _wordWithTwoUsagesInCurrentChat.Text.ToLower()
            })).Returns(new List<Word>() {
                _wordWithTwoUsagesInCurrentChat
            });

            var fakeUsagesRepo = new Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>>();
            fakeUsagesRepo.Setup(repo => repo.GetByTelegramIdAndWordsList(_currentChat.TelegramID, new List<string>() {
                _wordWithTwoUsagesInCurrentChat.Text
            })).Returns(new List<WordUsedTimes>() {
                new WordUsedTimes {
                    WordID = _wordWithTwoUsagesInCurrentChat.ID,
                    ChatID = _currentChat.ID,
                    UsedTimes = 2
                }
            });

            var bot = createMockedClient(
                wordsRepo: fakeWordsRepo,
                usagesRepo: fakeUsagesRepo
            );
            var update = createUpdate(_currentChat.ID, $"/count {_wordWithTwoUsagesInCurrentChat.Text}", ChatType.Group);
            var action = bot.HandleUpdate(update);
            
            Assert.Equal($"<b>{_wordWithTwoUsagesInCurrentChat.Text.ToLower()}</b>: 2", action.Text);
        }

        [Fact]
        public void TestCountCommandWhenChannelUpdateDoesNotTrigger()
        {
            var bot = createMockedClient();
            var channelUpdate = createUpdate(_currentChat.ID, $"/count {_wordWithNoUsagesInCurrentChat.Text}", ChatType.Channel);
            var action = bot.HandleUpdate(channelUpdate);
            Assert.Equal(null, action);
        }

        [Fact]
        public void TestCountCommandWhenPrivateUpdateDoesNotTrigger()
        {
            var bot = createMockedClient();
            var channelUpdate = createUpdate(_currentChat.ID, $"/count {_wordWithNoUsagesInCurrentChat.Text}", ChatType.Private);
            var action = bot.HandleUpdate(channelUpdate);
            Assert.Equal(null, action);
        }

        [Fact]
        public void TestCountCommandWithNoUsagesFound()
        {
            var bot = createMockedClient();
            var update = createUpdate(_currentChat.ID, $"/count {_wordWithNoUsagesInCurrentChat.Text}", ChatType.Group);
            var action = bot.HandleUpdate(update);
            Assert.Equal($"<b>{_wordWithNoUsagesInCurrentChat.Text}</b>: not found", action.Text);
        }

        [Fact]
        public void TestCountCommandWithNoUsagesFoundFromSupergroup()
        {
            var bot = createMockedClient();
            var update = createUpdate(_currentChat.ID, $"/count {_wordWithNoUsagesInCurrentChat.Text}", ChatType.Supergroup);
            var action = bot.HandleUpdate(update);
            Assert.Equal($"<b>{_wordWithNoUsagesInCurrentChat.Text}</b>: not found", action.Text);
        }

        [Fact]
        public void TestCountCommandWithNoWordsGiven()
        {

        }

        [Fact]
        public void TestCountCommandWithFourWordsGiven()
        {

        }

        [Fact]
        public void TestCountCommandWithTooShortWordGiven()
        {
            var bot = createMockedClient();
            var update = createUpdate(_currentChat.ID, $"/count {_tooShortWord.Text}", ChatType.Group);
            var action = bot.HandleUpdate(update);
            Assert.Equal($"<b>{_tooShortWord.Text}</b>: too short", action.Text);
        }

        // var fakeWordsRepo = new Mock<IWordsRepository<Word, WordsCountBotDbContext>>();

        // fakeWordsRepo.Setup(repo => repo.GetByText(new List<string>{
        //     wordWithNoUsagesInCurrentChat.Text
        // })).Returns(new List<Word>());

        // fakeWordsRepo.Setup(repo => repo.GetByText(new List<string>{
        //     wordWithTwoUsagesInCurrentChat.Text
        // })).Returns(new List<Word> {
        //     wordWithTwoUsagesInCurrentChat
        // });

        // var fakeUsagesRepo = new Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>>();

        // fakeUsagesRepo.Setup(repo => repo.GetByTelegramIdAndWordsList(currentChat.TelegramID, new List<string> {
        //     wordWithTwoUsagesInCurrentChat.Text
        // })).Returns(new List<WordUsedTimes> {
        //     new WordUsedTimes {
        //         WordID = wordWithTwoUsagesInCurrentChat.ID,
        //         ChatID = currentChat.ID
        //     }
        // });

        // var bot = createMockedClient(
        //     wordsRepo: fakeWordsRepo
        // );

        // var update = createUpdate(currentChat.TelegramID, "/count kek", ChatType.Group);
        // var action = bot.HandleUpdate(update);
        // Assert.Equal("<b>kek</b>: not found", action.Text);

        // update = createUpdate(currentChat.TelegramID, "/count kek", ChatType.Private);
        // action = bot.HandleUpdate(update);
        // Assert.Equal(action, null);

        // update = createUpdate(currentChat.TelegramID, "/count kek", ChatType.Channel);
        // action = bot.HandleUpdate(update);
        // Assert.Equal(action, null);

        // update = createUpdate(currentChat.TelegramID, "/count kek2", ChatType.Group);
        // action = bot.HandleUpdate(update);
        // Assert.Equal(action.Text, "<b>kek2</b>: 2"); //TODO: ожидать тут 2х использований

        /**
        TODO: заимплементить остальные тестовые случаи
            - слишком короткое слово
            - слово найдено
            - передано четыре слова в команде, в ответе должно быть три
        */
        // }

    }
}
