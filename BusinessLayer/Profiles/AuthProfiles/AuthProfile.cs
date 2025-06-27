using AutoMapper;
using BusinessLayer.DTOs.Auth;
using BusinessLayer.DTOs.Category;
using Domain.Entities;

namespace BusinessLayer.Profiles.AuthProfiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<User, LoginRequestDTO>().ReverseMap();
        CreateMap<User, LoginResponseDTO>().ReverseMap();
    }
}
