using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.Database;
using TelegramBot.Core.Models;
using TelegramBot.Core.Repositories;
using TelegramBot.Core.TelegramBot;
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

        Word _wordWithFourUsagesInCurrentChat = new Word
        {
            Text = "kekFourUsages",
            ID = 223
        };

        Word _wordWithFifteenUsagesInCurrentChat = new Word
        {
            Text = "kekPlentyOfUsages",
            ID = 224
        };

        Word _tooShortWord = new Word
        {
            Text = "le",
            ID = 220
        };

        TelegramBot.Core.Models.Chat _currentChat = new TelegramBot.Core.Models.Chat
        {
            ID = 333,
            TelegramID = -3456
        };

        private WcbTelegramBot createMockedClient(
            Mock<ILogger<WcbTelegramBot>> logger = null,
            Mock<IWordsRepository<Word, WordsCountBotDbContext>> wordsRepo = null,
            Mock<IChatsRepository<TelegramBot.Core.Models.Chat, WordsCountBotDbContext>> chatsRepo = null,
            Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>> usagesRepo = null,
            Mock<IWcbTelegramClient> client = null
        )
        {
            var _logger = logger ?? new Mock<ILogger<WcbTelegramBot>>();
            var _wordsRepo = wordsRepo ?? new Mock<IWordsRepository<Word, WordsCountBotDbContext>>();
            var _chatsRepo = chatsRepo ?? new Mock<IChatsRepository<TelegramBot.Core.Models.Chat, WordsCountBotDbContext>>();
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
                _wordWithTwoUsagesInCurrentChat.Text.ToLower() //Database contains lower case words only
            })).Returns(new List<Word>() {
                _wordWithTwoUsagesInCurrentChat
            }.Select(word =>
            {
                word.Text = word.Text.ToLower(); //Database contains lower case words only
                return word;
            }));

            var fakeUsagesRepo = new Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>>();
            fakeUsagesRepo.Setup(repo => repo.GetByTelegramIdAndWordsList(_currentChat.TelegramID, new List<string>() {
                _wordWithTwoUsagesInCurrentChat.Text.ToLower() //It is lowercased inside the handleCountMessage method
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
            var update = createUpdate(_currentChat.TelegramID, $"/count {_wordWithTwoUsagesInCurrentChat.Text}", ChatType.Group);
            var action = bot.HandleUpdate(update);

            Assert.Equal($"<b>{_wordWithTwoUsagesInCurrentChat.Text.ToLower()}</b>: 2", action.Text);
        }

        [Fact]
        public void TestCountCommandWhenChannelUpdateDoesNotTrigger()
        {
            var bot = createMockedClient();
            var channelUpdate = createUpdate(_currentChat.ID, $"/count {_wordWithNoUsagesInCurrentChat.Text}", ChatType.Channel);
            var action = bot.HandleUpdate(channelUpdate);
            Assert.Null(action);
        }

        [Fact]
        public void TestCountCommandWhenPrivateUpdateDoesNotTrigger()
        {
            var bot = createMockedClient();
            var channelUpdate = createUpdate(_currentChat.ID, $"/count {_wordWithNoUsagesInCurrentChat.Text}", ChatType.Private);
            var action = bot.HandleUpdate(channelUpdate);
            Assert.Null(action);
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
            var fakeUsagesRepo = new Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>>();
            fakeUsagesRepo
                .Setup(repo => repo.GetByTelegramIdTopWords(_currentChat.ID, 3))
                .Returns(new List<WordUsedTimes>(){
                    new WordUsedTimes { WordID = _wordWithTwoUsagesInCurrentChat.ID, ChatID = _currentChat.ID, UsedTimes = 2 },
                    new WordUsedTimes { WordID = _wordWithFourUsagesInCurrentChat.ID, ChatID = _currentChat.ID, UsedTimes = 4 },
                    new WordUsedTimes { WordID = _wordWithFifteenUsagesInCurrentChat.ID, ChatID = _currentChat.ID, UsedTimes = 15 }
                });

            var fakeWordsRepo = new Mock<IWordsRepository<Word, WordsCountBotDbContext>>();
            fakeWordsRepo
                .Setup(repo => repo.GetByID(new int[]{
                    _wordWithTwoUsagesInCurrentChat.ID,
                    _wordWithFourUsagesInCurrentChat.ID,
                    _wordWithFifteenUsagesInCurrentChat.ID
                }))
                .Returns(new List<Word>() {
                    _wordWithFifteenUsagesInCurrentChat,
                    _wordWithFourUsagesInCurrentChat,
                    _wordWithTwoUsagesInCurrentChat
                });

            var bot = createMockedClient(
                usagesRepo: fakeUsagesRepo,
                wordsRepo: fakeWordsRepo
            );

            var update = createUpdate(_currentChat.ID, $"/count", ChatType.Supergroup);
            var action = bot.HandleUpdate(update);
            var awaitedText = String.Join("\n", new string[]{
                $"<b>{_wordWithFifteenUsagesInCurrentChat.Text}</b>: 15",
                $"<b>{_wordWithFourUsagesInCurrentChat.Text}</b>: 4",
                $"<b>{_wordWithTwoUsagesInCurrentChat.Text}</b>: 2",
            });
            Assert.Equal(awaitedText, action.Text);
        }

        [Fact]
        public void TestCountCommandWithFourWordsGiven()
        {
            var fakeUsagesRepo = new Mock<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>>();
            fakeUsagesRepo
                .Setup(repo => repo.GetByTelegramIdTopWords(_currentChat.ID, 3))
                .Returns(new List<WordUsedTimes>(){
                    new WordUsedTimes { WordID = _wordWithTwoUsagesInCurrentChat.ID, ChatID = _currentChat.ID, UsedTimes = 2 },
                    new WordUsedTimes { WordID = _wordWithFourUsagesInCurrentChat.ID, ChatID = _currentChat.ID, UsedTimes = 4 },
                    new WordUsedTimes { WordID = _wordWithFifteenUsagesInCurrentChat.ID, ChatID = _currentChat.ID, UsedTimes = 15 }
                });

            var fakeWordsRepo = new Mock<IWordsRepository<Word, WordsCountBotDbContext>>();
            fakeWordsRepo
                .Setup(repo => repo.GetByID(new int[]{
                    _wordWithTwoUsagesInCurrentChat.ID,
                    _wordWithFourUsagesInCurrentChat.ID,
                    _wordWithFifteenUsagesInCurrentChat.ID
                }))
                .Returns(new List<Word>() {
                    _wordWithFifteenUsagesInCurrentChat,
                    _wordWithFourUsagesInCurrentChat,
                    _wordWithTwoUsagesInCurrentChat
                });

            var bot = createMockedClient(
                usagesRepo: fakeUsagesRepo,
                wordsRepo: fakeWordsRepo
            );
        
            var update = createUpdate(_currentChat.ID, $"/count {_wordWithFifteenUsagesInCurrentChat.Text} {_wordWithFourUsagesInCurrentChat.Text} {_wordWithTwoUsagesInCurrentChat.Text} {_wordWithNoUsagesInCurrentChat.Text}", ChatType.Group);
            var action = bot.HandleUpdate(update);
            Assert.Equal(3, action.Text.Split("\n").Length);
        }

        [Fact]
        public void TestCountCommandWithTooShortWordGiven()
        {
            var bot = createMockedClient();
            var update = createUpdate(_currentChat.ID, $"/count {_tooShortWord.Text}", ChatType.Group);
            var action = bot.HandleUpdate(update);
            Assert.Equal($"<b>{_tooShortWord.Text}</b>: too short", action.Text);
        }
    }
}
