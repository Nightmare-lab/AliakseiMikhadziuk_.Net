using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarPark.BLL.Dto;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Service
{
    public class ContractService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.Contract> _repository;

        public ContractService(IRepository<DAL.Models.Contract> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Contract contracts)
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

            _repository.Add(contract);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Edit(Contract contracts)
        {
            var contract = _mapper.Map<DAL.Models.Contract>(contracts);
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "Not exist!");
            }

            var updateModel = _repository.GetAll().FirstOrDefault(item => item.Id == contract.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(contract), "Object to update does not exist");
            }

            if (contract.StarTimeContract > contract.EndTimeContract)
            {
                throw new ArgumentException("Invalid contract date!");
            }

            _repository.Edit(contract);
        }

        public IEnumerable<Contract> GetAll()
        {
            return (_repository.GetAll()).Select(item => _mapper.Map<Contract>(item));
        }

        public Contract Get(int id)
        {
            return _mapper.Map<Contract>(_repository.Get(id));
        }
    }
}