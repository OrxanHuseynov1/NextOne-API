using FluentValidation;
using BusinessLayer.DTOs.Depo;

namespace BusinessLayer.Validators.DepoValidators;

public class DepoPostDTOValidator : AbstractValidator<DepoPostDTO>
{
    public DepoPostDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Depo adı boş ola bilməz.")
            .NotNull().WithMessage("Depo adı daxil edilməlidir.")
            .Length(3, 100).WithMessage("Depo adı ən az 3, ən çox 100 simvol olmalıdır.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}