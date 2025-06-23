using FluentValidation;
using BusinessLayer.DTOs.Returun;
using BusinessLayer.Validators.ReturnItemValidator;

namespace BusinessLayer.Validators.ReturnValidator;

public class ReturnPostDTOValidator : AbstractValidator<ReturunPostDTO>
{
    public ReturnPostDTOValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Müştəri ID-si boş ola bilməz.")
            .NotNull().WithMessage("Müştəri ID-si daxil edilməlidir.");

        RuleFor(x => x.PaymentType)
            .IsInEnum().WithMessage("Ödəniş növü düzgün deyil.");

        RuleFor(x => x.DebtReduction)
            .GreaterThanOrEqualTo(0).When(x => x.DebtReduction.HasValue)
            .WithMessage("Borc azaldılması mənfi ola bilməz.");

        RuleFor(x => x.ReturnItems)
            .NotEmpty().WithMessage("Qaytarılan məhsullar boş ola bilməz.")
            .NotNull().WithMessage("Qaytarılan məhsullar daxil edilməlidir.");

        RuleForEach(x => x.ReturnItems)
            .SetValidator(new ReturnItemPostDTOValidator()); 

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}