using AutoMapper;
using Talabaty.API.DTOS;
using Talabaty.DAL.Entities;

namespace Talabaty.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name)) // to return name of ProductBrand in ProductDTO
                .ForMember(d => d.ProductType, o => o.MapFrom(p => p.ProductType.Name))  // to return name of ProductType in ProductDTO
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());  // to return the domain of the API with PictureUrl in ProductDTO
        }
    }
}
