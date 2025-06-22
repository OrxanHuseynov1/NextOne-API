using AutoMapper;
using BusinessLayer.DTOs.Loss;
using Domain.Entities;

namespace BusinessLayer.Profiles.LossProfiles;

public class LossProfile : Profile
{
    public LossProfile()
    {
        CreateMap<LossGetDTO, Loss>().ReverseMap();
        CreateMap<LossPostDTO, Loss>().ReverseMap();
    }
}
