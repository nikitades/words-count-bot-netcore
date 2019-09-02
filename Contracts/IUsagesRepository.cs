using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IUsagesRepository<TEntity, TContext> : IRepository<WordUsedTimes, WordsCountBotDbContext>
    {

    }
}