using BusinessLayer.DTOs.Returun;

namespace BusinessLayer.Services.Abstractions;

public interface IReturnService
{
    Task CreateReturnAsync(ReturunPostDTO ReturnPostDTO);
    Task DeleteReturnAsync(Guid id);
    Task SoftDeleteReturnAsync(Guid id);
    Task RestoreReturnAsync(Guid id);
    Task<ICollection<ReturunGetDTO>> GetAllSoftDeletedReturn();
    Task<ICollection<ReturunGetDTO>> GetAllActiveReturnAsync();
    Task<ReturunGetDTO> GetByIdReturnAsync(Guid id);
}
