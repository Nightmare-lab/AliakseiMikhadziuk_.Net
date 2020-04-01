using System;
using System.Linq;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Models;

namespace CarPark.BLL.Service
{
    public class AccidentService
    {
        private readonly IRepository<Accidents> _repository;

        public AccidentService(IRepository<Accidents> repository)
        {
            _repository = repository;
        }

        public void Add(Accidents accident)
        {
            if (accident == null)
            {
                throw  new ArgumentNullException(nameof(accident),"Not exist!");
            }

            _repository.Add(accident);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Edit(Accidents accident)
        {
            if (accident == null)
            {
                throw new ArgumentNullException(nameof(accident), "Not exist!");
            }

            var updateModel = _repository.GetAll().FirstOrDefault(item => item.Id == accident.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(accident), "Object to update does not exist");
            }

            _repository.Edit(accident);
        }

    }
}