using Domain.Common;

namespace Domain.Entities;

public class ReturnItem : AuditableCompanyEntity
{
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int OrderIndex { get; set; }
    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
    public Guid ReturnId { get; set; }
    public required Return Return { get; set; }
}
