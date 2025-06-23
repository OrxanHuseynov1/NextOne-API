using AutoMapper;
using BusinessLayer.DTOs.Depo;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class DepoService : IDepoService
{
    private readonly IDepoReadRepository _depoReadRepository;
    private readonly IDepoWriteRepository _depoWriteRepository;
    private readonly IMapper _mapper;

    public DepoService(IDepoReadRepository depoReadRepository, IDepoWriteRepository depoWriteRepository, IMapper mapper)
    {
        _depoReadRepository = depoReadRepository;
        _depoWriteRepository = depoWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateDepoAsync(DepoPostDTO DepoPostDTO)
    {
        Depo depo = _mapper.Map<Depo>(DepoPostDTO);

        await _depoWriteRepository.CreateAsync(depo);
        var result = await _depoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Depo could not be created.");
        }
    }

    public async Task DeleteDepoAsync(Guid id)
    {
        if (!await _depoReadRepository.IsExist(id)) throw new Exception("Depo not found.");
        Depo depo = await _depoReadRepository.GetByIdAsync(id) ?? throw new Exception("Depo not found.");

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
        depo.IsDeleted = true;
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
        depo.IsDeleted = false;
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
}