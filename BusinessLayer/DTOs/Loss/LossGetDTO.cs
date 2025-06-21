
namespace BusinessLayer.DTOs.Loss;

public class LossGetDTO
{
    public Guid Id { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? Reason { get; set; }
    public DateTime LossDate { get; set; }
    public required string CreatedBy { get; set; }
    public Guid ProductId { get; set; }
    public required Domain.Entities.Product Product {  get; set; }
    public Guid DepoId { get; set; }
    public required Domain.Entities.Depo Depo { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
}
