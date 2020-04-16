using AutoMapper;

namespace CarPark.BLL.MappingProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DAL.Models.Car, Dto.Car>().ReverseMap();
            CreateMap<DAL.Models.Accident, Dto.Accident>().ReverseMap();
            CreateMap<DAL.Models.Contract, Dto.Contract>().ReverseMap();
        }
    }
}