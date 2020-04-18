using System.Threading.Tasks;

namespace CarPark.ConsoleUI.Interfaces
{
    public interface ICrudService<TEntity>
        where TEntity : class
    {
        Task AddAsync();

        Task RemoveAsync();

        Task EditAsync();

        Task<TEntity> CreateAsync();
    }
}