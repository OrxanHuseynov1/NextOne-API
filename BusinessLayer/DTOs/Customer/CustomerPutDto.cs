namespace BusinessLayer.DTOs.Customer;

public class CustomerPutDTO
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}
