using System;
using System.Linq;
using AutoMapper;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Service
{
    public class CarService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.Cars> _repository;

        public CarService(IRepository<DAL.Models.Cars> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Dto.Cars cars)
        {
            var car = _mapper.Map<DAL.Models.Cars>(cars);
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

        public void Edit(Dto.Cars cars)
        {
            var car = _mapper.Map<DAL.Models.Cars>(cars);
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
    }
}