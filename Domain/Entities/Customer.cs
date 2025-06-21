using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Customer : AuditableCompanyEntity
{
    public required string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public SaleType BuyType { get; set; }
    public decimal DebtAmount { get; set; }
    public ICollection<Sale>? Sales { get; set; } = [];
    public ICollection<DebtRecord>? DebtRecords { get; set; } = []; 
}
