namespace WordsCountBot.Contracts
{
    public interface ICreateRepository<TEntity> where TEntity : IModel
    {
        TEntity Create(TEntity t);
    }
}