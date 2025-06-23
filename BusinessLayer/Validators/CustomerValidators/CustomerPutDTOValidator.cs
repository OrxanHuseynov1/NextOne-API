using FluentValidation;
using BusinessLayer.DTOs.Customer;

namespace BusinessLayer.Validators.CustomerValidators;

public class CustomerPutDTOValidator : AbstractValidator<CustomerPutDTO>
{
    public CustomerPutDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID boş ola bilməz.")
            .NotNull().WithMessage("ID daxil edilməlidir.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Ad və Soyad boş ola bilməz.")
            .NotNull().WithMessage("Ad və Soyad daxil edilməlidir.")
            .Length(3, 100).WithMessage("Ad və Soyad ən az 3, ən çox 100 simvol olmalıdır.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Telefon nömrəsi ən çox 20 simvol ola bilər.")
            .Matches(@"^(\+?994|0)?(50|51|55|57|70|77|99|10|12)\s?\d{3}[\s-]?\d{2}[\s-]?\d{2}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Telefon nömrəsi düzgün formatda deyil. Nümunə: +994501234567, 050 123 45 67, 050-123-45-67.");

        RuleFor(x => x.Address)
            .MaximumLength(250).WithMessage("Ünvan ən çox 250 simvol ola bilər.");
    }
}