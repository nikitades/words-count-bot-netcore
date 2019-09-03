using System.Collections.Generic;
using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IUsagesRepository<TEntity, TContext> : IRepository<WordUsedTimes, WordsCountBotDbContext>
    {
        void IncrementLinks(IEnumerable<Word> words, Chat chat);
    }
}