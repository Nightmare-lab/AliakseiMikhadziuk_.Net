using System;
using System.Linq;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Models;

namespace CarPark.BLL.Service
{
    public class CarService
    {
        private readonly IRepository<Cars> _repository;

        public CarService(IRepository<Cars> repository)
        {
            _repository = repository;
        }

        public void Add(Cars car)
        {
            if (car == null)
            {
                throw  new ArgumentNullException(nameof(car),"Not exist!");
            }

            var isCarRegistrationNumberUnique = _repository.GetAll()
                .Any(item =>  item.CarRegistrationNumber == car.CarRegistrationNumber);

            if (isCarRegistrationNumberUnique)
            {
                throw new ArgumentException("CarRegistrationNumber is not unique!"); 
            }

            if (car.Price < 0)
            {
                throw  new ArgumentException("Price cannot be negative!");
            }
                
            _repository.Add(car);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Edit(Cars car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car), "Not exist!");
            }

            var updateModel = _repository.GetAll().FirstOrDefault(item => item.Id == car.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(car), "Object to update does not exist");
            }

            var isCarRegistrationNumberUnique = _repository.GetAll()
                .Any(item => item.CarRegistrationNumber == car.CarRegistrationNumber);

            if (isCarRegistrationNumberUnique)
            {
                throw new ArgumentException("CarRegistrationNumber is not unique!");
            }

            _repository.Edit(updateModel);
        }
    }
}