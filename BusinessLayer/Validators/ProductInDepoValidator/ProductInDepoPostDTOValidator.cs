using FluentValidation;
using BusinessLayer.DTOs.ProductInDepo;

namespace BusinessLayer.Validators.ProductInDepoValidator;

public class ProductInDepoPostDTOValidator : AbstractValidator<ProductInDepoPostDTO>
{
    public ProductInDepoPostDTOValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Miqdar boş ola bilməz.")
            .GreaterThan(0).WithMessage("Miqdar sıfırdan böyük olmalıdır.");

        RuleFor(x => x.DepoId)
            .NotEmpty().WithMessage("Depo ID-si boş ola bilməz.")
            .NotNull().WithMessage("Depo ID-si daxil edilməlidir.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Məhsul ID-si boş ola bilməz.")
            .NotNull().WithMessage("Məhsul ID-si daxil edilməlidir.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}