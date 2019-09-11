using System.Collections.Generic;
using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IUsagesRepository<TEntity, TContext> : IRepository<WordUsedTimes, WordsCountBotDbContext>
    {
        void IncrementLinks(IEnumerable<Word> words, Chat chat);
        IEnumerable<WordUsedTimes> GetByTelegramIdAndWordsList(long telegramId, IEnumerable<string> wordsList);
        IEnumerable<WordUsedTimes> GetByTelegramIdTopWords(long telegramId, int limit);
    }
}