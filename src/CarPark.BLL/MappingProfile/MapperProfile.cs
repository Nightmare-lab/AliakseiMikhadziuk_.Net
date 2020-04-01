using AutoMapper;

namespace CarPark.BLL.MappingProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DAL.Models.Cars, Dto.Cars>();
            CreateMap<DAL.Models.CarModels, Dto.CarModels>();
            CreateMap<DAL.Models.Accidents, Dto.Accidents>();
            CreateMap<DAL.Models.Contracts, Dto.Contract>();
            CreateMap<DAL.Models.Cars, Dto.Cars>().ReverseMap();
            CreateMap<DAL.Models.CarModels, Dto.CarModels>().ReverseMap();
            CreateMap<DAL.Models.Accidents, Dto.Accidents>().ReverseMap();
            CreateMap<DAL.Models.Contracts, Dto.Contract>().ReverseMap();
        }
    }
}