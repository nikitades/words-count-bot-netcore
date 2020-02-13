namespace TelegramBot.Core.Contracts
{
    public interface IDeleteRepository<TEntity> where TEntity : IModel
    {
        void Delete(TEntity t);
        void Delete(int ID);
    }
}