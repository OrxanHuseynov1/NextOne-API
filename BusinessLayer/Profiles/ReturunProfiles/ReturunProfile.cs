using AutoMapper;
using BusinessLayer.DTOs.Returun;
using Domain.Entities;

namespace BusinessLayer.Profiles.ReturunProfiles;

public class ReturunProfile : Profile
{
    public ReturunProfile()
    {
        CreateMap<ReturunGetDTO, Return>().ReverseMap();
        CreateMap<ReturunPostDTO, Return>().ReverseMap();
    }
}
