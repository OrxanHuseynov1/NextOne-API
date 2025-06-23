using AutoMapper;
using BusinessLayer.DTOs.Product;
using BusinessLayer.DTOs.ReturunItem;
using Domain.Entities;

namespace BusinessLayer.Profiles.ReturunItemProfiles;

public class ReturunItemProfile : Profile
{
    public ReturunItemProfile()
    {
        CreateMap<ReturunItemPostDTO, ReturnItem>().ReverseMap();
        CreateMap<ReturnItemGetDTO, ReturnItem>().ReverseMap();
    }
}
