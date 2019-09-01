using System.Collections.Generic;
using Contracts;
using WordsCountBot.Database;

namespace Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        private WordsCountBotDbContext _ctx;
        public AbstractRepository(WordsCountBotDbContext context)
        {
            _ctx = context;
        }
        public abstract void Create(T t);

        public abstract void Delete(T t);

        public abstract T Find(int i);

        public abstract ICollection<T> FindAll();

        public abstract void Update(T t);
    }
}