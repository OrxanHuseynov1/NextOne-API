using Domain.Common;

namespace Domain.Entities;

public class ProductInDepo : AuditableCompanyEntity
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid DepoId { get; set; }
    public Depo Depo { get; set; }
}
