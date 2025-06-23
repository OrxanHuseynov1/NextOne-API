
using BusinessLayer.DTOs.Company;

namespace BusinessLayer.Services.Abstractions;

public interface ICompanyService
{
    Task RestoreCompanyAsync(Guid id);
    Task UpdateCompanyAsync(CompanyPutDTO CompanyPutDTO);
    Task<ICollection<CompanyGetDTO>> GetAllActiveCompanyAsync();
    Task<CompanyGetDTO> GetByIdCompanyAsync(Guid id);
}
