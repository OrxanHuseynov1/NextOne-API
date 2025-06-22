using AutoMapper;
using BusinessLayer.DTOs.Category;
using Domain.Entities;

namespace BusinessLayer.Profiles.CategoryProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryGetDTO, Category>().ReverseMap();
        CreateMap<CategoryPostDTO, Category>().ReverseMap();
        CreateMap<CategoryPutDTO, Category>().ReverseMap();
    }
}
