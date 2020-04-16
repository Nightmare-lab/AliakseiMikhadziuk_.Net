using System.Collections.Generic;
using System.Linq;
using CarPark.DAL.EF;
using CarPark.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarPark.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CarParkContext _context;

        public Repository(CarParkContext db)
        {
            _context = db;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(item=>item.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking().ToList();
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Edit(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var entity = Get(id);
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }
}