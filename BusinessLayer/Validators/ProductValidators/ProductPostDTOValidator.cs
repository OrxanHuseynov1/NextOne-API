using FluentValidation;
using BusinessLayer.DTOs.Product;
using BusinessLayer.Validators.ProductInDepoValidator;

namespace BusinessLayer.Validators.ProductValidators;

public class ProductPostDTOValidator : AbstractValidator<ProductPostDTO>
{
    public ProductPostDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Məhsul adı boş ola bilməz.")
            .NotNull().WithMessage("Məhsul adı daxil edilməlidir.")
            .Length(3, 150).WithMessage("Məhsul adı ən az 3, ən çox 150 simvol olmalıdır.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıqlama ən çox 500 simvol ola bilər.");

        RuleFor(x => x.PurchasePrice)
            .NotEmpty().WithMessage("Alış qiyməti boş ola bilməz.")
            .GreaterThanOrEqualTo(0).WithMessage("Alış qiyməti mənfi ola bilməz.");

        RuleFor(x => x.WholesalePrice)
            .NotEmpty().WithMessage("Topdan satış qiyməti boş ola bilməz.")
            .GreaterThanOrEqualTo(0).WithMessage("Topdan satış qiyməti mənfi ola bilməz.")
            .GreaterThanOrEqualTo(x => x.PurchasePrice).WithMessage("Topdan satış qiyməti alış qiymətindən az ola bilməz.");

        RuleFor(x => x.RetailPrice)
            .NotEmpty().WithMessage("Pərakəndə satış qiyməti boş ola bilməz.")
            .GreaterThanOrEqualTo(0).WithMessage("Pərakəndə satış qiyməti mənfi ola bilməz.")
            .GreaterThanOrEqualTo(x => x.WholesalePrice).WithMessage("Pərakəndə satış qiyməti topdan satış qiymətindən az ola bilməz.");

        RuleFor(x => x.Barcode)
            .MaximumLength(50).WithMessage("Barkod ən çox 50 simvol ola bilər.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().When(x => x.CategoryId.HasValue)
            .WithMessage("Kateqoriya ID-si boş ola bilməz.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");

        RuleForEach(x => x.ProductInDepos)
            .SetValidator(new ProductInDepoPostDTOValidator()) 
            .When(x => x.ProductInDepos != null && x.ProductInDepos.Any());
    }
}