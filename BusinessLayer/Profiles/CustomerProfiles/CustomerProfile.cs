using AutoMapper;
using BusinessLayer.DTOs.Category;
using BusinessLayer.DTOs.Customer;
using Domain.Entities;

namespace BusinessLayer.Profiles.CustomerProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerGetDTO, Customer>().ReverseMap();
        CreateMap<CustomerPostDTO, Customer>().ReverseMap();
        CreateMap<CustomerPutDTO, Customer>().ReverseMap();

    }
}
