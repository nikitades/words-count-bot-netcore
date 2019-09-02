namespace WordsCountBot.Contracts
{
    public interface IRepository<TEntity> :
        ICreateRepository<TEntity>,
        IReadRepository<TEntity>,
        IUpdateRepository<TEntity>,
        IDeleteRepository<TEntity>
            where TEntity : IModel
    {
        /** 
            TODO: 
                - implement abstract full repository
                - implement real full repository for words
        */
    }
}