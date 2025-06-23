using FluentValidation;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.Validators.UserValidators;

public class UserPutDTOValidator : AbstractValidator<UserPutDTO>
{
    public UserPutDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID boş ola bilməz.")
            .NotNull().WithMessage("ID daxil edilməlidir.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Tam ad boş ola bilməz.")
            .NotNull().WithMessage("Tam ad daxil edilməlidir.")
            .Length(3, 100).WithMessage("Tam ad ən az 3, ən çox 100 simvol olmalıdır.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz.")
            .NotNull().WithMessage("İstifadəçi adı daxil edilməlidir.")
            .MinimumLength(3).WithMessage("İstifadəçi adı ən az 3 simvol olmalıdır.")
            .MaximumLength(50).WithMessage("İstifadəçi adı ən çox 50 simvol olmalıdır.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Rol növü düzgün deyil.");
    }
}