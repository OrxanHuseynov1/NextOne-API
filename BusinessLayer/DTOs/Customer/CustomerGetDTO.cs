using Domain.Entities;
using Domain.Enums;
namespace BusinessLayer.DTOs.Customer;

public class CustomerGetDTO
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public SaleType BuyType { get; set; }
    public decimal CurrentDebt { get; set; }
    public Guid CompanyId { get; set; }
    public required Domain.Entities.Company Company { get; set; }
    public ICollection<Sale> Sales { get; set; } = [];
    public ICollection<Domain.Entities.DebtRecord> Debts { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
}
