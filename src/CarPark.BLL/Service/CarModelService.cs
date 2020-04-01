using System;
using System.Linq;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Models;

namespace CarPark.BLL.Service
{
    public class CarModelService
    {
        private readonly IRepository<CarModels> _repository;

        public CarModelService(IRepository<CarModels> repository)
        {
            _repository = repository;
        }

        public void Add(CarModels carModel)
        {
            if (carModel == null)
            {
                throw new ArgumentNullException(nameof(carModel), "Not exist!");
            }

            var isCarModelExist = _repository.GetAll()
                .Any(item => item.Class == carModel.Class 
                             && item.Model == carModel.Model
                             && item.CarMake == carModel.CarMake);
            if (isCarModelExist)
            {
                throw new ArgumentException("Car Model Exist!");
            }

            _repository.Add(carModel);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Edit(CarModels carModel)
        {
            if (carModel == null)
            {
                throw new ArgumentNullException(nameof( carModel), "Not exist!");
            }

            var updateModel = _repository.GetAll().FirstOrDefault(item => item.Id == carModel.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(carModel), "Object to update does not exist");
            }

            var isCarModelExist = _repository.GetAll()
                .Any(item => item.Class == carModel.Class
                             && item.Model == carModel.Model
                             && item.CarMake == carModel.CarMake);
            if (isCarModelExist)
            {
                throw new ArgumentException("Car Model Exist!");
            }

            _repository.Edit(carModel);
        }
    }
}