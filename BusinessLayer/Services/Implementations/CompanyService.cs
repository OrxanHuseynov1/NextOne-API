using AutoMapper;
using BusinessLayer.DTOs.Company;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class CompanyService : ICompanyService
{
    private readonly ICompanyReadRepository _companyReadRepository;
    private readonly ICompanyWriteRepository _companyWriteRepository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyReadRepository companyReadRepository, ICompanyWriteRepository companyWriteRepository, IMapper mapper)
    {
        _companyReadRepository = companyReadRepository;
        _companyWriteRepository = companyWriteRepository;
        _mapper = mapper;
    }

    public async Task RestoreCompanyAsync(Guid id)
    {
        if (!await _companyReadRepository.IsExist(id)) throw new Exception("Company not found.");
        Company company = await _companyReadRepository.GetOneByCondition(c => c.Id == id && c.IsDeleted, false)
                            ?? throw new Exception("Company not found.");
        company.IsDeleted = false;
        _companyWriteRepository.Update(company);

        var result = await _companyWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Company could not be restored.");
        }
    }

    public async Task UpdateCompanyAsync(CompanyPutDTO CompanyPutDTO)
    {
        if (!await _companyReadRepository.IsExist(CompanyPutDTO.Id)) throw new Exception("Company not found.");

        Company companyToUpdate = await _companyReadRepository.GetByIdAsync(CompanyPutDTO.Id)
                                    ?? throw new Exception("Company not found.");

        _mapper.Map(CompanyPutDTO, companyToUpdate);
        _companyWriteRepository.Update(companyToUpdate);

        var result = await _companyWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Company could not be updated.");
        }
    }

    public async Task<ICollection<CompanyGetDTO>> GetAllActiveCompanyAsync()
    {
        ICollection<Company> companies = await _companyReadRepository.GetAllByCondition(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<CompanyGetDTO>>(companies);
    }

    public async Task<CompanyGetDTO> GetByIdCompanyAsync(Guid id)
    {
        if (!await _companyReadRepository.IsExist(id)) throw new Exception("Company not found.");
        Company company = await _companyReadRepository.GetByIdAsync(id) ?? throw new Exception("Company not found.");
        return _mapper.Map<CompanyGetDTO>(company);
    }
}