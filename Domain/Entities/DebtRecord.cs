using Domain.Common;

namespace Domain.Entities;

public class DebtRecord : AuditableCompanyEntity
{
    public double Amount { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
}
