using TelegramBot.Core.Database;
using TelegramBot.Core.Models;

namespace TelegramBot.Core.Contracts
{
    public interface IChatsRepository<TEntity, TContext> : IRepository<Chat, WordsCountBotDbContext>
    {

    }
}