using FluentValidation;
using BusinessLayer.DTOs.Loss;

namespace BusinessLayer.Validators.LossValidator;

public class LossPostDTOValidator : AbstractValidator<LossPostDTO>
{
    public LossPostDTOValidator()
    {
        RuleFor(x => x.UnitPrice)
            .NotEmpty().WithMessage("Vahid qiymət boş ola bilməz.")
            .GreaterThanOrEqualTo(0).WithMessage("Vahid qiymət mənfi ola bilməz.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Miqdar boş ola bilməz.")
            .GreaterThan(0).WithMessage("Miqdar sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage("Səbəb ən çox 500 simvol ola bilər.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Məhsul ID-si boş ola bilməz.")
            .NotNull().WithMessage("Məhsul ID-si daxil edilməlidir.");

        RuleFor(x => x.DepoId)
            .NotEmpty().WithMessage("Depo ID-si boş ola bilməz.")
            .NotNull().WithMessage("Depo ID-si daxil edilməlidir.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}