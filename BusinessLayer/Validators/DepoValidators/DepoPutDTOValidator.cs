using FluentValidation;
using BusinessLayer.DTOs.Depo;

namespace BusinessLayer.Validators.DepoValidators;

public class DepoPutDTOValidator : AbstractValidator<DepoPutDTO>
{
    public DepoPutDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID boş ola bilməz.")
            .NotNull().WithMessage("ID daxil edilməlidir.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Depo adı boş ola bilməz.")
            .NotNull().WithMessage("Depo adı daxil edilməlidir.")
            .Length(3, 100).WithMessage("Depo adı ən az 3, ən çox 100 simvol olmalıdır.");
    }
}