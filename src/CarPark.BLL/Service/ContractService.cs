using System;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Models;

namespace CarPark.BLL.Service
{
    public class ContractService
    {
        private readonly IRepository<Contracts> _repository;

        public ContractService(IRepository<Contracts> repository)
        {
            _repository = repository;
        }

        public void Add(Contracts contract)
        {
            if(contract == null)
            {
                throw new ArgumentNullException(nameof(contract), "Not exist!");
            }


        }
    }
}