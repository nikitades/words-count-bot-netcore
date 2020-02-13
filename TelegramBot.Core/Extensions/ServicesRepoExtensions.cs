using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.Database;
using TelegramBot.Core.Models;
using TelegramBot.Core.Repositories;

namespace TelegramBot.Core.Extensions
{
    public static class ServicesRepoExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IWordsRepository<Word, WordsCountBotDbContext>, WordsRepository>();
            services.AddTransient<IChatsRepository<Chat, WordsCountBotDbContext>, ChatsRepository>();
            services.AddTransient<IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>, UsagesRepository>();
        }
    }
}