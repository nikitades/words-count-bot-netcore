namespace TelegramBot.Core.Contracts
{
    public interface ICreateRepository<TEntity> where TEntity : IModel
    {
        void Create(TEntity t);
    }
}