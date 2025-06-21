using Domain.Entities;

namespace BusinessLayer.DTOs.DebtRecord;

public class DebtRecordGetDTO
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public required Domain.Entities.Customer Customer { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public bool IsDeleted { get; set; } = false;

}
