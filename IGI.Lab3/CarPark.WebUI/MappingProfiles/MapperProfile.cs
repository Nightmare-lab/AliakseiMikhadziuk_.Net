using AutoMapper;
using CarPark.WebUI.ViewModels;

namespace CarPark.WebUI.MappingProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BLL.Models.Car, CarViewModel>().ReverseMap();
            CreateMap<BLL.Models.Accident, AccidentViewModel>().ReverseMap();
            CreateMap<BLL.Models.Contract, ContractViewModel>().ReverseMap();
        }
    }
}
