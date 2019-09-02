using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IWordsRepository<TEntity> : IRepository<TEntity> where TEntity : Word
    {
        
    }
}