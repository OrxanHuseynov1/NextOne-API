using BusinessLayer.DTOs.Transfer;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace BusinessLayer.Services.Implementations;

public class TransferService : ITransferService
{
    private readonly ITransferReadRepository _transferReadRepository;
    private readonly ITransferWriteRepository _transferWriteRepository;
    private readonly IMapper _mapper;

    public TransferService(ITransferReadRepository transferReadRepository, ITransferWriteRepository transferWriteRepository, IMapper mapper)
    {
        _transferReadRepository = transferReadRepository;
        _transferWriteRepository = transferWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateTransferAsync(TransferPostDTO TransferPostDTO)
    {
        Transfer transfer = _mapper.Map<Transfer>(TransferPostDTO);

        await _transferWriteRepository.CreateAsync(transfer);
        var result = await _transferWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to create transfer.");
        }
    }

    public async Task DeleteTransferAsync(Guid id)
    {
        if (!await _transferReadRepository.IsExist(id)) throw new Exception("Transfer not found.");
        Transfer transfer = await _transferReadRepository.GetByIdAsync(id) ?? throw new Exception("Transfer not found.");

        _transferWriteRepository.Delete(transfer);

        var result = await _transferWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to delete transfer.");
        }
    }

    public async Task SoftDeleteTransferAsync(Guid id)
    {
        if (!await _transferReadRepository.IsExist(id)) throw new Exception("Transfer not found.");
        Transfer transfer = await _transferReadRepository.GetOneByCondition(t => t.Id == id && !t.IsDeleted, false)
                               ?? throw new Exception("Transfer not found.");
        transfer.IsDeleted = true;
        _transferWriteRepository.Update(transfer);

        var result = await _transferWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to soft delete transfer.");
        }
    }

    public async Task RestoreTransferAsync(Guid id)
    {
        if (!await _transferReadRepository.IsExist(id)) throw new Exception("Transfer not found.");
        Transfer transfer = await _transferReadRepository.GetOneByCondition(t => t.Id == id && t.IsDeleted, false)
                               ?? throw new Exception("Transfer not found.");
        transfer.IsDeleted = false;
        _transferWriteRepository.Update(transfer);

        var result = await _transferWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to restore transfer.");
        }
    }

    public async Task<ICollection<TransferGetDTO>> GetAllSoftDeletedTransfer()
    {
        ICollection<Transfer> transfers = await _transferReadRepository.GetAllByCondition(t => t.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<TransferGetDTO>>(transfers);
    }

    public async Task<ICollection<TransferGetDTO>> GetAllActiveTransferAsync()
    {
        ICollection<Transfer> transfers = await _transferReadRepository.GetAllByCondition(t => !t.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<TransferGetDTO>>(transfers);
    }

    public async Task<TransferGetDTO> GetByIdTransferAsync(Guid id)
    {
        if (!await _transferReadRepository.IsExist(id)) throw new Exception("Transfer not found.");
        Transfer transfer = await _transferReadRepository.GetByIdAsync(id) ?? throw new Exception("Transfer not found.");
        return _mapper.Map<TransferGetDTO>(transfer);
    }

    public async Task<ICollection<TransferGetDTO>> GetTransferHistoryByCompanyIdAsync(Guid companyId)
    {
        ICollection<Transfer> transfers = await _transferReadRepository
                                                    .GetAllByCondition(t => t.CompanyId == companyId && !t.IsDeleted)
                                                    .Include(t => t.Product)
                                                    .Include(t => t.FromDepo)
                                                    .Include(t => t.ToDepo)
                                                    .ToListAsync();
        return _mapper.Map<ICollection<TransferGetDTO>>(transfers);
    }
}