using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Sale : AuditableCompanyEntity
{
    public PaymentType PaymentType { get; set; }
    public decimal? TotalDiscount { get; set; }
    public decimal? DebtLeft { get; set; }
    public Status SaleStatus { get; set; } = Status.Approved;
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<SaleItem> SaleItems { get; set; }
}
