
using BusinessLayer.DTOs.DebtRecord;

namespace BusinessLayer.Services.Abstractions;

public interface IDebtRecordService
{
    Task CreateDebtRecordAsync(DebtRecordPostDTO DebtRecordPostDTO);
    Task DeleteDebtRecordAsync(Guid id);
    Task SoftDeleteDebtRecordAsync(Guid id);
    Task RestoreDebtRecordAsync(Guid id);
    Task<ICollection<DebtRecordGetDTO>> GetAllSoftDeletedDebtRecord();
    Task<ICollection<DebtRecordGetDTO>> GetAllActiveDebtRecordAsync();
    Task<DebtRecordGetDTO> GetByIdDebtRecordAsync(Guid id);
}
