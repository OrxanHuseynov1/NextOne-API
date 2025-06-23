using BusinessLayer.DTOs.Category;
using FluentValidation;

namespace BusinessLayer.Validators.CategoryValidators;

public class CategoryPostDTOValidator : AbstractValidator<CategoryPostDTO>
{
    public CategoryPostDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz.")
            .NotNull().WithMessage("Kateqoriya adı daxil edilməlidir.")
            .Length(3, 100).WithMessage("Kateqoriya adı ən az 3, ən çox 100 simvol olmalıdır.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıqlama ən çox 500 simvol ola bilər.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}
