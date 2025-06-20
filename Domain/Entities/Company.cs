using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Company : AuditableEntity
{
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public List<ModuleType> ModuleTypes { get; set; } = [];
    public DateTime PackageEndDate { get; set; }
    public bool AutoSubtractStock { get; set; }
    public SaleType SaleType { get; set; } = SaleType.Both;
    public bool IsReceptionUpdate { get; set; } = false;
    public ReceiptType ReceiptType { get; set; } = ReceiptType.A4;
    public string? InstagramLink { get; set; }
    public string? TikTokLink { get; set; }
    public string? Address { get; set; }
    public bool ShowAddressOnReceipt { get; set; } = false;
}
