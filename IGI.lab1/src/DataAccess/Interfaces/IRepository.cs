using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Create(T item);
    }
}