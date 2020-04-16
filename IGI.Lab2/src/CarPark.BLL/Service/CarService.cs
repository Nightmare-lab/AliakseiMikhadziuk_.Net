using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarPark.BLL.Dto;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Service
{
    public class CarService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.Car> _repository;

        public CarService(IRepository<DAL.Models.Car> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Car cars)
        {
            var car = _mapper.Map<DAL.Models.Car>(cars);
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car), "Not exist!");
            }

            var isCarRegistrationNumberUnique = _repository.GetAll()
                .Any(item => item.CarRegistrationNumber == car.CarRegistrationNumber);

            if (isCarRegistrationNumberUnique)
            {
                throw new ArgumentException("CarRegistrationNumber is not unique!");
            }

            if (car.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative!");
            }

            _repository.Add(car);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Edit(Car cars)
        {
            var car = _mapper.Map<DAL.Models.Car>(cars);
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

            _repository.Edit(car);
        }

        public IEnumerable<Car> GetAll()
        {
            return (_repository.GetAll()).Select(item => _mapper.Map<Car>(item));
        }

        public Car Get(int id)
        {
            return _mapper.Map<Car>(_repository.Get(id));
        }
    }
}