using Domain.Entities;
using Domain.Enums;

namespace BusinessLayer.DTOs.Sale;

public class SaleGetDTO
{
    public Guid Id { get; set; }
    public decimal? TotalDiscount { get; set; }
    public decimal? DebtLeft { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public PaymentType PaymentType { get; set; }
    public Status SaleStatus { get; set; }
    public ICollection<Domain.Entities.SaleItem> SaleItems { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }

}
