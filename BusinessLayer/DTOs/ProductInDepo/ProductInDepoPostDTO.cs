namespace BusinessLayer.DTOs.ProductInDepo;

public class ProductInDepoPostDTO
{
    public int Quantity { get; set; }
    public Guid DepoId { get; set; }
    public Guid ProductId { get; set; }
    public Guid CompanyId { get; set; }
}
