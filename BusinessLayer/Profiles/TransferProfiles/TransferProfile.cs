using AutoMapper;
using BusinessLayer.DTOs.Sale;
using BusinessLayer.DTOs.Transfer;
using Domain.Entities;

namespace BusinessLayer.Profiles.TransferProfiles;

public class TransferProfile : Profile
{
    public TransferProfile()
    {
        CreateMap<TransferGetDTO, Transfer>().ReverseMap();
        CreateMap<TransferPostDTO, Transfer>().ReverseMap();

    }
}
