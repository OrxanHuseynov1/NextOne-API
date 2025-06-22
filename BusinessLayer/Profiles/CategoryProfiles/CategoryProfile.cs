using AutoMapper;
using BusinessLayer.DTOs.Category;
using Domain.Entities;

namespace BusinessLayer.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryGetDTO, Category>().ReverseMap();
        CreateMap<CategoryPostDTO, Category>().ReverseMap();
        CreateMap<CategoryPutDTO, Category>().ReverseMap();
    }
}
