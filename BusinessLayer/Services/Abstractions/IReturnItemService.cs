using BusinessLayer.DTOs.ReturunItem;

namespace BusinessLayer.Services.Abstractions;

public interface IReturnItemService
{
    Task CreateReturnItemAsync(ReturunItemPostDTO ReturnItemPostDTO);
    Task DeleteReturnItemAsync(Guid id);
    Task SoftDeleteReturnItemAsync(Guid id);
    Task RestoreReturnItemAsync(Guid id);
    Task<ICollection<ReturnItemGetDTO>> GetAllSoftDeletedReturnItem();
    Task<ICollection<ReturnItemGetDTO>> GetAllActiveReturnItemAsync();
    Task<ReturnItemGetDTO> GetByIdReturnItemAsync(Guid id);
}
