using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.BLL.Models;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Services
{
    public class CarService : IService<Car>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.Car> _repository;

        public CarService(IRepository<DAL.Models.Car> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(Car cars)
        {
            var car = _mapper.Map<DAL.Models.Car>(cars);
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car), "Not exist!");
            }

            var isCarRegistrationNumberUnique = (await _repository.GetAllAsync())
                .Any(item => item.CarRegistrationNumber == car.CarRegistrationNumber);

            if (isCarRegistrationNumberUnique)
            {
                throw new ArgumentException("CarRegistrationNumber is not unique!");
            }

            if (car.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative!");
            }

            await _repository.AddAsync(car);
        }

        public async Task RemoveAsync(int id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task EditAsync(Car cars)
        {
            var car = _mapper.Map<DAL.Models.Car>(cars);
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car), "Not exist!");
            }

            var updateModel = (await _repository.GetAllAsync()).FirstOrDefault(item => item.Id == car.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(car), "Object to update does not exist");
            }

            var isCarRegistrationNumberUnique = (await _repository.GetAllAsync())
                .Any(item => item.CarRegistrationNumber == car.CarRegistrationNumber);

            if (isCarRegistrationNumberUnique)
            {
                throw new ArgumentException("CarRegistrationNumber is not unique!");
            }

            await _repository.EditAsync(car);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return ( await _repository.GetAllAsync()).Select(item => _mapper.Map<Car>(item));
        }

        public async Task<Car> GetAsync(int id)
        {
            return _mapper.Map<Car>(await _repository.GetAsync(id));
        }
    }
}