using BusinessLayer.DTOs.Product;

namespace BusinessLayer.Services.Abstractions;

public interface IProductService
{
    Task CreateProductAsync(ProductPostDTO ProductPostDTO);
    Task DeleteProductAsync(Guid id);
    Task SoftDeleteProductAsync(Guid id);
    Task RestoreProductAsync(Guid id);
    Task UpdateCustomerAsync(ProductPutDTO customerPutDTO);
    Task<ICollection<ProductGetDTO>> GetAllSoftDeletedProduct();
    Task<ICollection<ProductGetDTO>> GetAllActiveProductAsync();
    Task<ProductGetDTO> GetByIdProductAsync(Guid id);
}
