using System;
using System.Linq;
using AutoMapper;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Service
{
    public class CarModelService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.CarModels> _repository;

        public CarModelService(IRepository<DAL.Models.CarModels> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Dto.CarModels carModels)
        {
            var carModel = _mapper.Map<DAL.Models.CarModels>(carModels);
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

        public void Edit(Dto.CarModels carModels)
        {
            var carModel = _mapper.Map<DAL.Models.CarModels>(carModels);
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