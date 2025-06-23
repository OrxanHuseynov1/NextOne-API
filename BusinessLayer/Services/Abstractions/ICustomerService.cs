using BusinessLayer.DTOs.Customer;

namespace BusinessLayer.Services.Abstractions;

public interface ICustomerService
{
    Task CreateCustomerAsync(CustomerPostDTO customerPostDTO);
    Task DeleteCustomerAsync(Guid id);
    Task SoftDeleteCustomerAsync(Guid id);
    Task RestoreCustomerAsync(Guid id);
    Task UpdateCustomerAsync(CustomerPutDTO customerPutDTO);
    Task<ICollection<CustomerGetDTO>> GetAllSoftDeletedCustomer();
    Task<ICollection<CustomerGetDTO>> GetAllActiveCustomerAsync();
    Task<CustomerGetDTO> GetByIdCustomerAsync(Guid id);
}
