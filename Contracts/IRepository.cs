using System.Collections.Generic;

namespace Contracts
{
    public interface IRepository<T>
    {
        ICollection<T> FindAll();
        T Find(int i);
        void Create(T t);
        void Update(T t);
        void Delete(T t);
    }
}