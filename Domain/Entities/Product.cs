using Domain.Common;

namespace Domain.Entities;

public class Product : AuditableEntity
{
    public string? Barcode { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }

    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<ProductInDepo> ProductInDepos { get; set; }
    public Guid CompanyId { get; set; }
    public required Company Company { get; set; }

}
