using AutoMapper;
using BusinessLayer.DTOs.Expense;
using Domain.Entities;

namespace BusinessLayer.Profiles.ExpenseProfiles;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<ExpensePostDTO, Expense>().ReverseMap();
        CreateMap<ExpenseGetDTO, Expense>().ReverseMap();
    }
}
