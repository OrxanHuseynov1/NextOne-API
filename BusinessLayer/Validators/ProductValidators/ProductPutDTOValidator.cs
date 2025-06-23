using FluentValidation;
using BusinessLayer.DTOs.Product;

namespace BusinessLayer.Validators.ProductValidators;

public class ProductPutDTOValidator : AbstractValidator<ProductPutDTO>
{
    public ProductPutDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID boş ola bilməz.")
            .NotNull().WithMessage("ID daxil edilməlidir.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Məhsul adı boş ola bilməz.")
            .NotNull().WithMessage("Məhsul adı daxil edilməlidir.")
            .Length(3, 150).WithMessage("Məhsul adı ən az 3, ən çox 150 simvol olmalıdır.");

        RuleFor(x => x.Barcode)
            .MaximumLength(50).WithMessage("Barkod ən çox 50 simvol ola bilər.");

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

        RuleFor(x => x.CategoryId)
            .NotEmpty().When(x => x.CategoryId.HasValue)
            .WithMessage("Kateqoriya ID-si boş ola bilməz.");
    }
}