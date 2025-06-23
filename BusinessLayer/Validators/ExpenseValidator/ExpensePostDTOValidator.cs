using FluentValidation;
using BusinessLayer.DTOs.Expense;

namespace BusinessLayer.Validators.ExpenseValidator;

public class ExpensePostDTOValidator : AbstractValidator<ExpensePostDTO>
{
    public ExpensePostDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .NotNull().WithMessage("Başlıq daxil edilməlidir.")
            .Length(3, 100).WithMessage("Başlıq ən az 3, ən çox 100 simvol olmalıdır.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Məbləğ boş ola bilməz.")
            .GreaterThan(0).WithMessage("Məbləğ sıfırdan böyük olmalıdır.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıqlama ən çox 500 simvol ola bilər.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}