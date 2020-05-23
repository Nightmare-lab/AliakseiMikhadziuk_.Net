using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.BLL.Models;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Services
{
    public class AccidentService : IService<Accident>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.Accident> _repository;

        public AccidentService(IRepository<DAL.Models.Accident> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(Accident accidents)
        {
            var accident = _mapper.Map<DAL.Models.Accident>(accidents);
            if (accident == null)
            {
                throw  new ArgumentNullException(nameof(accident),"Not exist!");
            }

            await _repository.AddAsync(accident);
        }

        public async Task RemoveAsync(int id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task EditAsync(Accident accidents)
        {
            var accident = _mapper.Map<DAL.Models.Accident>(accidents);
            if (accident == null)
            {
                throw new ArgumentNullException(nameof(accident), "Not exist!");
            }

            var updateModel = (await _repository.GetAllAsync()).FirstOrDefault(item => item.Id == accident.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(accident), "Object to update does not exist");
            }

            await _repository.EditAsync(accident);
        }

        public async Task<Accident> GetAsync(int id)
        {
            return _mapper.Map<Accident>(await _repository.GetAsync(id));
        }

        public async Task<IEnumerable<Accident>> GetAllAsync()
        {
            return (await _repository.GetAllAsync()).Select(item => _mapper.Map<Accident>(item));
        }
    }
}