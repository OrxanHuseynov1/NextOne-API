using AutoMapper;
using BusinessLayer.DTOs.Returun;
using BusinessLayer.DTOs.SaleItem;
using Domain.Entities;

namespace BusinessLayer.Profiles.SaleItemProfiles;

public class SaleItemProfile : Profile
{
    public SaleItemProfile()
    {
        CreateMap<SaleItemPostDTO, SaleItem>().ReverseMap();
    }
}
