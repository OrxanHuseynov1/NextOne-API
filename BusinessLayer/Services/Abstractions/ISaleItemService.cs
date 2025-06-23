using BusinessLayer.DTOs.SaleItem;

namespace BusinessLayer.Services.Abstractions;

public interface ISaleItemService
{
    Task CreateSaleItemAsync(SaleItemPostDTO SaleItemPostDTO);
    Task DeleteSaleItemAsync(Guid id);
    Task SoftDeleteSaleItemAsync(Guid id);
    Task RestoreSaleItemAsync(Guid id);
    Task<ICollection<SaleItemGetDTO>> GetAllSoftDeletedSaleItem();
    Task<ICollection<SaleItemGetDTO>> GetAllActiveSaleItemAsync();
    Task<SaleItemGetDTO> GetByIdSaleItemAsync(Guid id);
}
