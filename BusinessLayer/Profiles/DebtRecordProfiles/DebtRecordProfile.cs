using AutoMapper;
using BusinessLayer.DTOs.Customer;
using BusinessLayer.DTOs.DebtRecord;
using Domain.Entities;

namespace BusinessLayer.Profiles.DebtRecordProfiles;

public class DebtRecordProfile : Profile
{
    public DebtRecordProfile() 
    {
        CreateMap<DebtRecordGetDTO, DebtRecord>().ReverseMap();
        CreateMap<DebtRecordPostDTO, DebtRecord>().ReverseMap();
    }
}
