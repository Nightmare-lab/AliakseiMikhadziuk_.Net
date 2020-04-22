using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.BLL.Models;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Services
{
    public class ContractService : IService<Contract>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.Contract> _repository;

        public ContractService(IRepository<DAL.Models.Contract> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(Contract contracts)
        {
            var contract = _mapper.Map<DAL.Models.Contract>(contracts);

            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "Not exist!");
            }

            if (contract.StarTimeContract > contract.EndTimeContract)
            {
                throw new ArgumentException("Invalid contract date!");
            }

            await _repository.AddAsync(contract);
        }

        public async Task RemoveAsync(int id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task EditAsync(Contract contracts)
        {
            var contract = _mapper.Map<DAL.Models.Contract>(contracts);
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "Not exist!");
            }

            var updateModel = (await _repository.GetAllAsync()).FirstOrDefault(item => item.Id == contract.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(contract), "Object to update does not exist");
            }

            if (contract.StarTimeContract > contract.EndTimeContract)
            {
                throw new ArgumentException("Invalid contract date!");
            }

            await _repository.EditAsync(contract);
        }

        public async Task<IEnumerable<Contract>> GetAllAsync()
        {
            return ( await _repository.GetAllAsync()).Select(item => _mapper.Map<Contract>(item));
        }

        public async Task<Contract> GetAsync(int id)
        {
            return _mapper.Map<Contract>(await _repository.GetAsync(id));
        }
    }
}