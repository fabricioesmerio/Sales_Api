

using AutoMapper;
using Sales.Application.Dto;
using Sales.Domain.Entities;

namespace Sales.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<SaleItem, SaleItemDto>().ReverseMap();
        }
    }
}
