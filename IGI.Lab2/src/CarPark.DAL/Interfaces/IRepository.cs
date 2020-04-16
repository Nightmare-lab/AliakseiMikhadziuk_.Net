using System.Collections.Generic;

namespace CarPark.DAL.Interfaces
{
    public interface IRepository <TEntity> where TEntity : class, IEntity
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void Remove(int id);

        void Edit(TEntity entity);
    }
}
