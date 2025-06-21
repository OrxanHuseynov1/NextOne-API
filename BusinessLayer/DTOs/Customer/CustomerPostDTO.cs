using Domain.Enums;

namespace BusinessLayer.DTOs.Customer;

public class CustomerPostDTO
{
    public required string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public SaleType? BuyType { get; set; }
    public Guid CompanyId { get; set; }
}
