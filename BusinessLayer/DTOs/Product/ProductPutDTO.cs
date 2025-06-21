namespace BusinessLayer.DTOs.Product;

public class ProductPutDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public Guid? CategoryId { get; set; }
}
