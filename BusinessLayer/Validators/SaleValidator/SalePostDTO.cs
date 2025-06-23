using FluentValidation;
using BusinessLayer.DTOs.Sale;
using BusinessLayer.DTOs.SaleItem;
using Domain.Enums;
using BusinessLayer.Validators.SaleItemValidator;

namespace BusinessLayer.Validators.SaleValidator;

public class SalePostDTOValidator : AbstractValidator<SalePostDTO>
{
    public SalePostDTOValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Müştəri ID-si boş ola bilməz.")
            .NotNull().WithMessage("Müştəri ID-si daxil edilməlidir.");

        RuleFor(x => x.PaymentType)
            .IsInEnum().WithMessage("Ödəniş növü düzgün deyil.");

        RuleFor(x => x.TotalDiscount)
            .GreaterThanOrEqualTo(0).When(x => x.TotalDiscount.HasValue)
            .WithMessage("Ümumi endirim mənfi ola bilməz.");

        RuleFor(x => x.DebtLeft)
            .GreaterThanOrEqualTo(0).When(x => x.DebtLeft.HasValue)
            .WithMessage("Qalan borc mənfi ola bilməz.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");

        RuleFor(x => x.SaleStatus)
            .IsInEnum().WithMessage("Satış statusu düzgün deyil.");

        RuleFor(x => x.SaleItems)
            .NotEmpty().WithMessage("Satış məhsulları boş ola bilməz.")
            .NotNull().WithMessage("Satış məhsulları daxil edilməlidir.");

        RuleForEach(x => x.SaleItems)
            .SetValidator(new SaleItemPostDTOValidator()); 

        RuleFor(x => x.CreatedBy)
            .MaximumLength(100).WithMessage("Yaradanın adı ən çox 100 simvol ola bilər.");
    }
}