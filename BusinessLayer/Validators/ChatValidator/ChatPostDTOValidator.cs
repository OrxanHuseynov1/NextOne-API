using BusinessLayer.DTOs.Chat;
using FluentValidation;

namespace BusinessLayer.Validators.ChatValidator;

public class ChatPostDTOValidator : AbstractValidator<ChatPostDTO>
{
    public ChatPostDTOValidator()
    {
        RuleFor(x => x.Sender)
            .NotEmpty().WithMessage("Mesajı Göndərən adı boş ola bilməz.")
            .NotNull().WithMessage("Mesajı Göndərən daxil edilməlidir.")
            .MinimumLength(2).WithMessage("Mesajı Göndərən ən az 2 simvol olmalıdır.")
            .MaximumLength(50).WithMessage("Mesajı Göndərən ən çox 50 simvol olmalıdır.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Mesaj boş ola bilməz.")
            .NotNull().WithMessage("Mesaj daxil edilməlidir.")
            .MinimumLength(1).WithMessage("Mesaj ən az 1 simvol olmalıdır.")
            .MaximumLength(1000).WithMessage("Mesaj ən çox 1000 simvol ola bilər.");

        RuleFor(x => x.CompanyId)
            .NotEmpty().WithMessage("Şirkət ID-si boş ola bilməz.")
            .NotNull().WithMessage("Şirkət ID-si daxil edilməlidir.");
    }
}
