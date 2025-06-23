using BusinessLayer.DTOs.Auth;
using FluentValidation;

namespace BusinessLayer.Validators.AuthValidator;

public class LoginRequestDTOValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestDTOValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz.")
            .NotNull().WithMessage("İstifadəçi adı daxil edilməlidir.")
            .MinimumLength(2).WithMessage("İstifadəçi adı ən az 2 simvol olmalıdır."); 

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.")
            .NotNull().WithMessage("Şifrə daxil edilməlidir.")
            .MinimumLength(8).WithMessage("Şifrə ən az 8 simvol olmalıdır."); 
    }
}
