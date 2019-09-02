namespace WordsCountBot.Contracts
{
    public interface IRepository<TEntity, TContext> :
        ICreateRepository<TEntity>,
        IReadRepository<TEntity>,
        IUpdateRepository<TEntity>,
        IDeleteRepository<TEntity>
            where TEntity : IModel
    {
        TContext GetContext();
        /** 
            TODO: 
                - implement abstract full repository
                - implement real full repository for words
        */
    }
}