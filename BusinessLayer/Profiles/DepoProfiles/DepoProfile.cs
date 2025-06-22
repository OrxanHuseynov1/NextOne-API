using AutoMapper;
using BusinessLayer.DTOs.DebtRecord;
using BusinessLayer.DTOs.Depo;
using Domain.Entities;

namespace BusinessLayer.Profiles.DepoProfiles;

public class DepoProfile : Profile
{
    public DepoProfile()
    {
        CreateMap<DepoGetDTO, Depo>().ReverseMap();
        CreateMap<DepoPostDTO, Depo>().ReverseMap();
        CreateMap<DepoPutDTO, Depo>().ReverseMap();
    }
}
