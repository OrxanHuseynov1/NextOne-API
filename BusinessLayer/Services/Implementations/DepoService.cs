using AutoMapper;
using BusinessLayer.DTOs.Depo;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BusinessLayer.Services.Implementations;

public class DepoService : IDepoService
{
    private readonly IDepoReadRepository _depoReadRepository;
    private readonly IDepoWriteRepository _depoWriteRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DepoService(IDepoReadRepository depoReadRepository, IDepoWriteRepository depoWriteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _depoReadRepository = depoReadRepository;
        _depoWriteRepository = depoWriteRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateDepoAsync(DepoPostDTO depoPostDto)
    {
        var depo = _mapper.Map<Depo>(depoPostDto);
        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        depo.CreatedBy = currentUserId ?? "System";
        depo.CreatedAt = DateTime.UtcNow.AddHours(4);

        await _depoWriteRepository.CreateAsync(depo);
        await _depoWriteRepository.SaveAsync();
    }

    public async Task DeleteDepoAsync(Guid id)
    {
        if (!await _depoReadRepository.IsExist(id)) throw new Exception("Depo not found.");
        Depo depo = await _depoReadRepository.GetByIdAsync(id) ?? throw new Exception("Depo not found.");

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        depo.DeletedBy = currentUserId ?? "System";
        depo.DeletedAt = DateTime.UtcNow.AddHours(4);

        _depoWriteRepository.Delete(depo);

        var result = await _depoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Depo could not be deleted.");
        }
    }

    public async Task SoftDeleteDepoAsync(Guid id)
    {
        if (!await _depoReadRepository.IsExist(id)) throw new Exception("Depo not found.");
        Depo depo = await _depoReadRepository.GetOneByCondition(c => c.Id == id && !c.IsDeleted, false)
                                 ?? throw new Exception("Depo not found.");

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        depo.IsDeleted = true;
        depo.LastModifiedBy = currentUserId ?? "System";
        depo.LastModifiedAt = DateTime.UtcNow.AddHours(4);

        _depoWriteRepository.Update(depo);

        var result = await _depoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Depo could not be soft deleted.");
        }
    }

    public async Task RestoreDepoAsync(Guid id)
    {
        if (!await _depoReadRepository.IsExist(id)) throw new Exception("Depo not found.");
        Depo depo = await _depoReadRepository.GetOneByCondition(c => c.Id == id && c.IsDeleted, false)
                                 ?? throw new Exception("Depo not found.");

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        depo.IsDeleted = false;
        depo.LastModifiedBy = currentUserId ?? "System";
        depo.LastModifiedAt = DateTime.UtcNow.AddHours(4);

        _depoWriteRepository.Update(depo);

        var result = await _depoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Depo could not be restored.");
        }
    }

    public async Task UpdateDepoAsync(DepoPutDTO DepoPutDTO)
    {
        if (!await _depoReadRepository.IsExist(DepoPutDTO.Id)) throw new Exception("Depo not found.");

        Depo depoToUpdate = await _depoReadRepository.GetByIdAsync(DepoPutDTO.Id)
                                         ?? throw new Exception("Depo not found.");

        _mapper.Map(DepoPutDTO, depoToUpdate);

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        depoToUpdate.LastModifiedBy = currentUserId ?? "System";
        depoToUpdate.LastModifiedAt = DateTime.UtcNow.AddHours(4);

        _depoWriteRepository.Update(depoToUpdate);

        var result = await _depoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Depo could not be updated.");
        }
    }

    public async Task<ICollection<DepoGetDTO>> GetAllSoftDeletedDepo()
    {
        ICollection<Depo> depos = await _depoReadRepository.GetAllByCondition(c => c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<DepoGetDTO>>(depos);
    }

    public async Task<ICollection<DepoGetDTO>> GetAllActiveDepoAsync()
    {
        ICollection<Depo> depos = await _depoReadRepository.GetAllByCondition(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<DepoGetDTO>>(depos);
    }

    public async Task<DepoGetDTO> GetByIdDepoAsync(Guid id)
    {
        if (!await _depoReadRepository.IsExist(id)) throw new Exception("Depo not found.");
        Depo depo = await _depoReadRepository.GetByIdAsync(id) ?? throw new Exception("Depo not found.");
        return _mapper.Map<DepoGetDTO>(depo);
    }

    public async Task<int> GetActiveDepoCountByCompanyIdAsync(Guid companyId)
    {
        return await _depoReadRepository.GetAllByCondition(d => d.CompanyId == companyId && !d.IsDeleted).CountAsync();
    }

    public async Task<ICollection<DepoGetDTO>> GetAllDeposByCompanyIdAsync(Guid companyId)
    {
        ICollection<Depo> depos = await _depoReadRepository
            .GetAllByCondition(d => d.CompanyId == companyId && !d.IsDeleted)
            .ToListAsync();
        return _mapper.Map<ICollection<DepoGetDTO>>(depos);
    }

    public async Task<IEnumerable<DepoGetDTO>> GetPagedDeposByCompanyIdAsync(Guid companyId, int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        var depos = await _depoReadRepository
            .GetAllByCondition(d => d.CompanyId == companyId && !d.IsDeleted)
            .OrderBy(d => d.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<DepoGetDTO>>(depos);
    }

    public async Task<Dictionary<Guid, int>> GetDepoProductCountsByCompanyIdAsync(Guid companyId)
    {
        var deposWithProductQuantities = await _depoReadRepository
            .GetAllByCondition(d => d.CompanyId == companyId && !d.IsDeleted)
            .Include(d => d.ProductInDepos)
            .Select(d => new {
                d.Id,
                ProductCount = d.ProductInDepos.Sum(pi => pi.Quantity)
            })
            .ToListAsync();

        return deposWithProductQuantities.ToDictionary(d => d.Id, d => d.ProductCount);
    }
}