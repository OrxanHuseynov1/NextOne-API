using Domain.Entities;
using Domain.Enums;

namespace BusinessLayer.DTOs.Returun;

public class ReturunGetDTO
{
    public Guid Id { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal? DebtReduction { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<ReturnItem> Items { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
    public Guid CustomerId { get; set; }
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public required Domain.Entities.Customer Customer { get; set; }
}
