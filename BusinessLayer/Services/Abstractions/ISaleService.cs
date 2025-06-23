using BusinessLayer.DTOs.Sale;

namespace BusinessLayer.Services.Abstractions;

public interface ISaleService
{
    Task CreateSaleAsync(SalePostDTO SalePostDTO);
    Task DeleteSaleAsync(Guid id);
    Task SoftDeleteSaleAsync(Guid id);
    Task RestoreSaleAsync(Guid id);
    Task<ICollection<SaleGetDTO>> GetAllSoftDeletedSale();
    Task<ICollection<SaleGetDTO>> GetAllActiveSaleAsync();
    Task<SaleGetDTO> GetByIdSaleAsync(Guid id);
}
