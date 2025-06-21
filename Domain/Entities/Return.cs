using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Return : AuditableCompanyEntity
{
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal? DebtReduction { get; set; }
    public List<ReturnItem> Items { get; set; } = [];
}
