using BusinessLayer.DTOs.Loss;

namespace BusinessLayer.Services.Abstractions;

public interface ILossService
{
    Task CreateLossAsync(LossPostDTO LossPostDTO);
    Task DeleteLossAsync(Guid id);
    Task SoftDeleteLossAsync(Guid id);
    Task RestoreLossAsync(Guid id);
    Task<ICollection<LossGetDTO>> GetAllSoftDeletedLoss();
    Task<ICollection<LossGetDTO>> GetAllActiveLossAsync();
    Task<LossGetDTO> GetByIdLossAsync(Guid id);
}
