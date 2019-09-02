using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IWordsRepository<TEntity, TContext> : IRepository<Word, WordsCountBotDbContext>
    {

    }
}