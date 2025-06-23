namespace BusinessLayer.DTOs.ReturunItem;

public class ReturnItemGetDTO
{
    public Guid Id { get; set; } 
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int OrderIndex { get; set; }
    public Guid ProductId { get; set; }
    public Guid ReturnId { get; set; }
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public required Domain.Entities.Return Return{ get; set; }
    public required Domain.Entities.Product Product { get; set; }
}
