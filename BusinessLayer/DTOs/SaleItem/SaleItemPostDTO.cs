namespace BusinessLayer.DTOs.SaleItem;

public class SaleItemPostDTO
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public int BoxCount { get; set; }
    public decimal UnitPrice { get; set; }
    public int OrderIndex { get; set; }
    public Guid CompanyId { get; set; }

}
