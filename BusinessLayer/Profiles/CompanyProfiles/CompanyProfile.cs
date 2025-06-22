using AutoMapper;
using BusinessLayer.DTOs.Category;
using BusinessLayer.DTOs.Company;
using Domain.Entities;

namespace BusinessLayer.Profiles.CompanyProfiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyGetDTO, Company>().ReverseMap();
        CreateMap<CompanyPutDTO, Company>().ReverseMap();
    }
}
