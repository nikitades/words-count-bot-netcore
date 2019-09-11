using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Contracts
{
    public interface IChatsRepository<TEntity, TContext> : IRepository<Chat, WordsCountBotDbContext>
    {

    }
}