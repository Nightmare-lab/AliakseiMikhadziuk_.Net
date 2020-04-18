using AutoMapper;
using CarPark.BLL.Models;

namespace CarPark.BLL.MappingProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DAL.Models.Car, Car>().ReverseMap();
            CreateMap<DAL.Models.Accident, Accident>().ReverseMap();
            CreateMap<DAL.Models.Contract, Contract>().ReverseMap();
        }
    }
}