namespace BusinessLayer.DTOs.SaleItem;

public class SaleItemGetDTO
{
    public Guid Id { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int OrderIndex { get; set; }
    public Guid ProductId { get; set; }
    public Guid SaleId { get; set; }
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public required Domain.Entities.Sale Sale { get; set; }
    public required Domain.Entities.Product Product { get; set; }
}
