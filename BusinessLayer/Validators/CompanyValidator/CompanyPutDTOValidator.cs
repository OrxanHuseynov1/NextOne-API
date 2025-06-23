using BusinessLayer.DTOs.Company;
using FluentValidation;

namespace BusinessLayer.Validators.CompanyValidator;

public class CompanyPutDTOValidator : AbstractValidator<CompanyPutDTO>
{
    public CompanyPutDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Şirkət adı boş ola bilməz.")
            .NotNull().WithMessage("Şirkət adı daxil edilməlidir.")
            .Length(3, 150).WithMessage("Şirkət adı ən az 3, ən çox 150 simvol olmalıdır.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Telefon nömrəsi ən çox 20 simvol ola bilər.")
            .Matches(@"^(\+?994|0)?(50|51|55|57|70|77|99|10|12|10)\s?\d{3}[\s-]?\d{2}[\s-]?\d{2}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Telefon nömrəsi düzgün formatda deyil. Nümunə: +994501234567, 050 123 45 67, 050-123-45-67.");

        RuleFor(x => x.SaleType)
            .IsInEnum().WithMessage("Satış növü düzgün deyil.");

        RuleFor(x => x.ReceiptType)
            .IsInEnum().WithMessage("Qəbz növü düzgün deyil.");

        RuleFor(x => x.InstagramLink)
            .MaximumLength(250).WithMessage("Instagram keçidi ən çox 250 simvol ola bilər.")
            .Matches(@"^(https?:\/\/)?(www\.)?instagram\.com\/[a-zA-Z0-9_\.]+\/?$").When(x => !string.IsNullOrEmpty(x.InstagramLink))
            .WithMessage("Instagram keçidi düzgün formatda deyil."); 

        RuleFor(x => x.TikTokLink)
            .MaximumLength(250).WithMessage("TikTok keçidi ən çox 250 simvol ola bilər.")
            .Matches(@"^(https?:\/\/)?(www\.)?tiktok\.com\/@?[a-zA-Z0-9_.]+\/?$").When(x => !string.IsNullOrEmpty(x.TikTokLink))
            .WithMessage("TikTok keçidi düzgün formatda deyil.");

        RuleFor(x => x.Address)
            .MaximumLength(250).WithMessage("Ünvan ən çox 250 simvol ola bilər.");
    }
}
