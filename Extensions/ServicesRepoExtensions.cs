using Microsoft.Extensions.DependencyInjection;
using WordsCountBot.Contracts;
using WordsCountBot.Database;
using WordsCountBot.Models;
using WordsCountBot.Repositories;

namespace WordsCountBot.Extensions
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