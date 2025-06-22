using AutoMapper;
using BusinessLayer.DTOs.Sale;
using Domain.Entities;

namespace BusinessLayer.Profiles.SaleProfiles;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<SaleGetDTO, Sale>().ReverseMap();
        CreateMap<SalePostDTO, Sale>().ReverseMap();
    }
}
