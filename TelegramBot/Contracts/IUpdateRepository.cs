namespace WordsCountBot.Contracts
{
    public interface IUpdateRepository<TEntity> where TEntity : IModel
    {
        void Update(TEntity t);
    }
}