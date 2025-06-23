using FluentValidation;
using BusinessLayer.DTOs.SaleItem;

namespace BusinessLayer.Validators.SaleItemValidator;

public class SaleItemPostDTOValidator : AbstractValidator<SaleItemPostDTO>
{
    public SaleItemPostDTOValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Məhsul ID-si boş ola bilməz.")
            .NotNull().WithMessage("Məhsul ID-si daxil edilməlidir.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Miqdar boş ola bilməz.")
            .GreaterThan(0).WithMessage("Miqdar sıfırdan böyük olmalıdır.");

        RuleFor(x => x.BoxCount)
            .GreaterThanOrEqualTo(0).WithMessage("Qutu sayı mənfi ola bilməz.");

        RuleFor(x => x.UnitPrice)
            .NotEmpty().WithMessage("Vahid qiymət boş ola bilməz.")
            .GreaterThanOrEqualTo(0).WithMessage("Vahid qiymət mənfi ola bilməz.");

        RuleFor(x => x.OrderIndex)
            .NotEmpty().WithMessage("Sıra indeksi boş ola bilməz.")
            .GreaterThanOrEqualTo(0).WithMessage("Sıra indeksi mənfi ola bilməz.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}