using Domain.Common;

namespace Domain.Entities;

public class Loss : AuditableCompanyEntity
{
    public int Quantity { get; set; }
    public string? Reason { get; set; }
    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public Guid DepoId { get; set; }
    public required Depo Depo { get; set; }
}
