using System;
using System.Collections.Generic;

namespace WordsCountBot.Contracts
{
    public interface IReadRepository<TEntity> where TEntity : IModel
    {
        TEntity GetOne(int ID);
        TEntity Get(TEntity t);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetBy(Func<TEntity, bool> predicate);
    }
}