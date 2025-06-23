using BusinessLayer.DTOs.ProductInDepo;

namespace BusinessLayer.Services.Abstractions;

public interface IProductInDepoService
{
    Task CreateProductInDepoAsync(ProductInDepoPostDTO ProductInDepoPostDTO);
    Task DeleteProductInDepoAsync(Guid id);
    Task SoftDeleteProductInDepoAsync(Guid id);
    Task RestoreProductInDepoAsync(Guid id);
    Task<ICollection<ProductInDepoGetDTO>> GetAllSoftDeletedProductInDepo();
    Task<ICollection<ProductInDepoGetDTO>> GetAllActiveProductInDepoAsync();
    Task<ProductInDepoGetDTO> GetByIdProductInDepoAsync(Guid id);
}
