namespace BusinessLayer.DTOs.Chat;

public class ChatPostDTO
{
    public required string Sender { get; set; } 
    public required string Message { get; set; } 
    public Guid CompanyId { get; set; }
}
