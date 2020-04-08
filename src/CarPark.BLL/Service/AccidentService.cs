using System;
using System.Linq;
using AutoMapper;
using CarPark.DAL.Interfaces;

namespace CarPark.BLL.Service
{
    public class AccidentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DAL.Models.Accidents> _repository;

        public AccidentService(IRepository<DAL.Models.Accidents> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(Dto.Accidents accidents)
        {
            var accident = _mapper.Map<DAL.Models.Accidents>(accidents);
            if (accident == null)
            {
                throw  new ArgumentNullException(nameof(accident),"Not exist!");
            }

            _repository.Add(accident);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void Edit(Dto.Accidents accidents)
        {
            var accident = _mapper.Map<DAL.Models.Accidents>(accidents);
            if (accident == null)
            {
                throw new ArgumentNullException(nameof(accident), "Not exist!");
            }

            var updateModel = _repository.GetAll().FirstOrDefault(item => item.Id == accident.Id);

            if (updateModel == null)
            {
                throw new ArgumentNullException(nameof(accident), "Object to update does not exist");
            }

            _repository.Edit(accident);
        }

    }
}