namespace BusinessLayer.DTOs.Expense;

public class ExpenseGetDTO
{
    public Guid Id { get; set; }
    public required string Title { get; set; } 
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
}
