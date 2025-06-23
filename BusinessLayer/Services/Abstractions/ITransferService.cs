using BusinessLayer.DTOs.Transfer;

namespace BusinessLayer.Services.Abstractions;

public interface ITransferService
{
    Task CreateTransferAsync(TransferPostDTO TransferPostDTO);
    Task DeleteTransferAsync(Guid id);
    Task SoftDeleteTransferAsync(Guid id);
    Task RestoreTransferAsync(Guid id);
    Task<ICollection<TransferGetDTO>> GetAllSoftDeletedTransfer();
    Task<ICollection<TransferGetDTO>> GetAllActiveTransferAsync();
    Task<TransferGetDTO> GetByIdTransferAsync(Guid id);
}
