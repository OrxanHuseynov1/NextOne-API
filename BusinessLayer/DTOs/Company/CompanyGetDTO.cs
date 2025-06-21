using Domain.Enums;

namespace BusinessLayer.DTOs.Company;

public class CompanyGetDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<ModuleType> ModuleTypes { get; set; } = [];
    public DateTime PackageEndDate { get; set; }
    public bool AutoSubtractStock { get; set; }
    public SaleType SaleType { get; set; }
    public bool IsReceptionUpdate { get; set; }
    public ReceiptType ReceiptType { get; set; }
    public string? InstagramLink { get; set; }
    public string? TikTokLink { get; set; }
    public string? Address { get; set; }
    public bool ShowAddressOnReceipt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
