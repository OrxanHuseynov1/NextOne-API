namespace BusinessLayer.DTOs.Expense;

public class ExpensePostDTO
{
    public required string Title { get; set; } 
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public Guid CompanyId { get; set; }

}
