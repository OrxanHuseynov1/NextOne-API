using BusinessLayer.DTOs.ProductInDepo;

namespace BusinessLayer.DTOs.Product;

public class ProductPostDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public string? Barcode { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid CompanyId { get; set; }
    public ICollection<ProductInDepoPostDTO>? ProductInDepos { get; set; } = [];

}
