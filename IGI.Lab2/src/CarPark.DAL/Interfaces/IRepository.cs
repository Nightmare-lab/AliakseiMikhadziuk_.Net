using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPark.DAL.Interfaces
{
    public interface IRepository <TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task RemoveAsync(int id);

        Task EditAsync(TEntity entity);
    }
}
