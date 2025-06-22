using Domain.Common;

namespace Domain.Entities;

public class SaleItem : AuditableCompanyEntity
{
    public int BoxCount { get; set; } = 1;
    public int Count { get; set; }
    public decimal UnitPrice { get; set; }
    public int OrderIndex { get; set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
