using System.Collections.Generic;
using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IWordsRepository<TEntity, TContext> : IRepository<Word, WordsCountBotDbContext>
    {
        void Create(IEnumerable<string> incomingWords);
        void Create(IEnumerable<Word> incomingWords);
        IEnumerable<Word> GetByText(IEnumerable<string> sourceWords);
        IEnumerable<Word> GetByID(IEnumerable<int> IDs);
    }
}