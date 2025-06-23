using AutoMapper;
using BusinessLayer.DTOs.DebtRecord;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class DebtRecordService : IDebtRecordService
{
    private readonly IDebtRecordReadRepository _debtRecordReadRepository;
    private readonly IDebtRecordWriteRepository _debtRecordWriteRepository;
    private readonly IMapper _mapper;

    public DebtRecordService(IDebtRecordReadRepository debtRecordReadRepository, IDebtRecordWriteRepository debtRecordWriteRepository, IMapper mapper)
    {
        _debtRecordReadRepository = debtRecordReadRepository;
        _debtRecordWriteRepository = debtRecordWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateDebtRecordAsync(DebtRecordPostDTO DebtRecordPostDTO)
    {
        DebtRecord debtRecord = _mapper.Map<DebtRecord>(DebtRecordPostDTO);

        await _debtRecordWriteRepository.CreateAsync(debtRecord);
        var result = await _debtRecordWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Debt record could not be created.");
        }
    }

    public async Task DeleteDebtRecordAsync(Guid id)
    {
        if (!await _debtRecordReadRepository.IsExist(id)) throw new Exception("Debt record not found.");
        DebtRecord debtRecord = await _debtRecordReadRepository.GetByIdAsync(id) ?? throw new Exception("Debt record not found.");

        _debtRecordWriteRepository.Delete(debtRecord);

        var result = await _debtRecordWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Debt record could not be deleted.");
        }
    }

    public async Task SoftDeleteDebtRecordAsync(Guid id)
    {
        if (!await _debtRecordReadRepository.IsExist(id)) throw new Exception("Debt record not found.");
        DebtRecord debtRecord = await _debtRecordReadRepository.GetOneByCondition(c => c.Id == id && !c.IsDeleted, false)
                            ?? throw new Exception("Debt record not found.");
        debtRecord.IsDeleted = true;
        _debtRecordWriteRepository.Update(debtRecord);

        var result = await _debtRecordWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Debt record could not be soft deleted.");
        }
    }

    public async Task RestoreDebtRecordAsync(Guid id)
    {
        if (!await _debtRecordReadRepository.IsExist(id)) throw new Exception("Debt record not found.");
        DebtRecord debtRecord = await _debtRecordReadRepository.GetOneByCondition(c => c.Id == id && c.IsDeleted, false)
                            ?? throw new Exception("Debt record not found.");
        debtRecord.IsDeleted = false;
        _debtRecordWriteRepository.Update(debtRecord);

        var result = await _debtRecordWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Debt record could not be restored.");
        }
    }

    public async Task<ICollection<DebtRecordGetDTO>> GetAllSoftDeletedDebtRecord()
    {
        ICollection<DebtRecord> debtRecords = await _debtRecordReadRepository.GetAllByCondition(c => c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<DebtRecordGetDTO>>(debtRecords);
    }

    public async Task<ICollection<DebtRecordGetDTO>> GetAllActiveDebtRecordAsync()
    {
        ICollection<DebtRecord> debtRecords = await _debtRecordReadRepository.GetAllByCondition(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<DebtRecordGetDTO>>(debtRecords);
    }

    public async Task<DebtRecordGetDTO> GetByIdDebtRecordAsync(Guid id)
    {
        if (!await _debtRecordReadRepository.IsExist(id)) throw new Exception("Debt record not found.");
        DebtRecord debtRecord = await _debtRecordReadRepository.GetByIdAsync(id) ?? throw new Exception("Debt record not found.");
        return _mapper.Map<DebtRecordGetDTO>(debtRecord);
    }
}