namespace BusinessLayer.DTOs.ProductInDepo;

public class ProductInDepoGetDTO
{
    public Guid Id { get; set; }
    public Guid DepoId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public required Domain.Entities.Depo Depo { get; set; }
    public required Domain.Entities.Product Product { get; set; }
}
