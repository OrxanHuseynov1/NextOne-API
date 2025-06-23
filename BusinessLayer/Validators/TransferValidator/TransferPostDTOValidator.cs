using FluentValidation;
using BusinessLayer.DTOs.Transfer;

namespace BusinessLayer.Validators.TransferValidator;

public class TransferPostDTOValidator : AbstractValidator<TransferPostDTO>
{
    public TransferPostDTOValidator()
    {
        RuleFor(x => x.FromDepoId)
            .NotEmpty().WithMessage("Göndərən Depo ID-si boş ola bilməz.")
            .NotNull().WithMessage("Göndərən Depo ID-si daxil edilməlidir.");

        RuleFor(x => x.ToDepoId)
            .NotEmpty().WithMessage("Qəbul edən Depo ID-si boş ola bilməz.")
            .NotNull().WithMessage("Qəbul edən Depo ID-si daxil edilməlidir.")
            .NotEqual(x => x.FromDepoId).WithMessage("Göndərən və qəbul edən Depo ID-ləri eyni ola bilməz.");

        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Məhsul ID-si boş ola bilməz.")
            .NotNull().WithMessage("Məhsul ID-si daxil edilməlidir.");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Miqdar boş ola bilməz.")
            .GreaterThan(0).WithMessage("Miqdar sıfırdan böyük olmalıdır.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}