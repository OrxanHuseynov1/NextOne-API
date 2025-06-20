using Domain.Common;

namespace Domain.Entities;

public class SaleItem : AuditableEntity
{
    public int BoxCount { get; set; } = 1;
    public int Count { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int OrderIndex { get; set; }
    public Guid SaleId { get; set; }
    public required Sale Sale { get; set; }
    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
}
