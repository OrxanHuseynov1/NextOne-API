using FluentValidation;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.Validators.UserValidators;

public class UserPostDTOValidator : AbstractValidator<UserPostDTO>
{
    public UserPostDTOValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Tam ad boş ola bilməz.")
            .NotNull().WithMessage("Tam ad daxil edilməlidir.")
            .Length(3, 100).WithMessage("Tam ad ən az 3, ən çox 100 simvol olmalıdır.");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("İstifadəçi adı boş ola bilməz.")
            .NotNull().WithMessage("İstifadəçi adı daxil edilməlidir.")
            .MinimumLength(3).WithMessage("İstifadəçi adı ən az 3 simvol olmalıdır.")
            .MaximumLength(50).WithMessage("İstifadəçi adı ən çox 50 simvol olmalıdır.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.")
            .NotNull().WithMessage("Şifrə daxil edilməlidir.")
            .MinimumLength(6).WithMessage("Şifrə ən az 6 simvol olmalıdır.")
            .Matches("[A-Z]").WithMessage("Şifrə ən az bir böyük hərf ehtiva etməlidir.")
            .Matches("[a-z]").WithMessage("Şifrə ən az bir kiçik hərf ehtiva etməlidir.")
            .Matches("[0-9]").WithMessage("Şifrə ən az bir rəqəm ehtiva etməlidir.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifrə ən az bir simvol ehtiva etməlidir.");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Rol növü düzgün deyil.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}