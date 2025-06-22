using AutoMapper;
using BusinessLayer.DTOs.ProductInDepo;
using Domain.Entities;

namespace BusinessLayer.Profiles.ProductInDepoProfiles;

public class ProductInDepoProfile : Profile
{
    public ProductInDepoProfile()
    {
        CreateMap<ProductInDepoGetDTO, ProductInDepo>().ReverseMap();
        CreateMap<ProductInDepoPostDTO, ProductInDepo>().ReverseMap();
    }
}
