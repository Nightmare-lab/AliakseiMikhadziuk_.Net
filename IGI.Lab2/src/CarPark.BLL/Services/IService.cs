using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPark.BLL.Services
{
    public interface IService<TEntity>
    {
        Task<TEntity> GetAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task RemoveAsync(int id);

        Task EditAsync(TEntity entity);
    }
}