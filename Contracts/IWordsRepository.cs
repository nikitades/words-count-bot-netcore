using System.Collections.Generic;
using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IWordsRepository<TEntity, TContext> : IRepository<Word, WordsCountBotDbContext>
    {
        void Create(IEnumerable<string> incomingWords);
        void Create(IEnumerable<Word> incomingWords);
    }
}