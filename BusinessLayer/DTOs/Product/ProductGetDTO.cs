using BusinessLayer.DTOs.ProductInDepo;

namespace BusinessLayer.DTOs.Product;

public class ProductGetDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public int Quantity { get; set; }
    public string? Barcode { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public ICollection<Domain.Entities.ProductInDepo>? ProductInDepos { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
}
