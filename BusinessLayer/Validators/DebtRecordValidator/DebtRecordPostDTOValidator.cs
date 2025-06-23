using FluentValidation;
using BusinessLayer.DTOs.DebtRecord;

namespace BusinessLayer.Validators.DebtRecordValidator;

public class DebtRecordPostDTOValidator : AbstractValidator<DebtRecordPostDTO>
{
    public DebtRecordPostDTOValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Məbləğ boş ola bilməz.")
            .GreaterThan(0).WithMessage("Məbləğ sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıqlama ən çox 500 simvol ola bilər.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Now).When(x => x.Date.HasValue)
            .WithMessage($"Tarix bu gündən (hal-hazırdan) gələcək olmamalıdır. Hal-hazırkı tarix: {DateTime.Now:dd.MM.yyyy HH:mm}");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().When(x => x.CustomerId.HasValue)
            .WithMessage("Müştəri ID-si boş ola bilməz.");
    }
}