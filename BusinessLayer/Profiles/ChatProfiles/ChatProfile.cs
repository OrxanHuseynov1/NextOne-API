using AutoMapper;
using BusinessLayer.DTOs.Chat;
using Domain.Entities;

namespace BusinessLayer.Profiles;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<ChatGetDTO, Chat>().ReverseMap();
        CreateMap<ChatPostDTO, Category>().ReverseMap();
    }
}
