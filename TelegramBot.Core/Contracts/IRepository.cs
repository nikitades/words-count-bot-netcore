namespace TelegramBot.Core.Contracts
{
    public interface IRepository<TEntity, TContext> :
        ICreateRepository<TEntity>,
        IReadRepository<TEntity>,
        IUpdateRepository<TEntity>,
        IDeleteRepository<TEntity>
            where TEntity : IModel
    {
        TContext GetContext();
    }
}