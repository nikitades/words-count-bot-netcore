using TelegramBot.Core.Models;

namespace TelegramBot.Core.Database
{
    public static class DbInitializer
    {
        public static void Initialize(WordsCountBotDbContext context)
        {
            
            var words = new Word[]{
                new Word { ID = 1, Text = "kek" },
                new Word { ID = 2, Text = "pek" },
                new Word { ID = 3, Text = "cheburek" },
            };
            context.Words.AddRange(words);

            var chats = new Chat[]{
                new Chat { ID = 1, TelegramID = 1243234, Name = "some chat name" },
                new Chat { ID = 2, TelegramID = -2342324, Name = "monus chat" },
                new Chat { ID = 3, TelegramID = -7728282, Name = "cool chat" },
            };
            context.Chats.AddRange(chats);

            var usages = new WordUsedTimes[] {
                new WordUsedTimes { ChatID = 1, WordID = 1, UsedTimes = 55 },
                new WordUsedTimes { ChatID = 2, WordID = 3, UsedTimes = 1 },
                new WordUsedTimes { ChatID = 3, WordID = 2, UsedTimes = 9999999 }
            };
            context.Usages.AddRange(usages);

            context.SaveChanges();
        }
    }
}