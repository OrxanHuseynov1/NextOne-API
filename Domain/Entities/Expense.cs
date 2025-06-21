using Domain.Common;

namespace Domain.Entities;

public class Expense : AuditableCompanyEntity
{
    public required string Title { get; set; } 
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
