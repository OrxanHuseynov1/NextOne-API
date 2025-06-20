using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Sale : AuditableEntity
{
    public PaymentType PaymentType { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal DebtLeft { get; set; }
    public Guid CompanyId { get; set; }
    public required Company Company { get; set; }
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public required ICollection<SaleItem> SaleItems { get; set; }
}
