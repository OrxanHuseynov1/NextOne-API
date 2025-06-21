namespace BusinessLayer.DTOs.Chat;

public class ChatGetDTO
{
    public Guid Id { get; set; }
    public required string Sender { get; set; }
    public required string Message { get; set; } 
    public DateTime CreatedAt { get; set; }
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public bool IsDeleted { get; set; } = false;
}
