using BusinessLayer.DTOs.Depo;

namespace BusinessLayer.Services.Abstractions;

public interface IDepoService
{
    Task CreateDepoAsync(DepoPostDTO DepoPostDTO);
    Task DeleteDepoAsync(Guid id);
    Task SoftDeleteDepoAsync(Guid id);
    Task RestoreDepoAsync(Guid id);
    Task UpdateDepoAsync(DepoPutDTO DepoPutDTO);
    Task<ICollection<DepoGetDTO>> GetAllSoftDeletedDepo();
    Task<ICollection<DepoGetDTO>> GetAllActiveDepoAsync();
    Task<DepoGetDTO> GetByIdDepoAsync(Guid id);
}
