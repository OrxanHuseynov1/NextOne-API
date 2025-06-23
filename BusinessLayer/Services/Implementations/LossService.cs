using AutoMapper;
using BusinessLayer.DTOs.Loss;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class LossService : ILossService
{
    private readonly ILossReadRepository _lossReadRepository;
    private readonly ILossWriteRepository _lossWriteRepository;
    private readonly IMapper _mapper;

    public LossService(ILossReadRepository lossReadRepository, ILossWriteRepository lossWriteRepository, IMapper mapper)
    {
        _lossReadRepository = lossReadRepository;
        _lossWriteRepository = lossWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateLossAsync(LossPostDTO LossPostDTO)
    {
        Loss loss = _mapper.Map<Loss>(LossPostDTO);

        await _lossWriteRepository.CreateAsync(loss);
        var result = await _lossWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Loss could not be created.");
        }
    }

    public async Task DeleteLossAsync(Guid id)
    {
        if (!await _lossReadRepository.IsExist(id)) throw new Exception("Loss not found.");
        Loss loss = await _lossReadRepository.GetByIdAsync(id) ?? throw new Exception("Loss not found.");

        _lossWriteRepository.Delete(loss);

        var result = await _lossWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Loss could not be deleted.");
        }
    }

    public async Task SoftDeleteLossAsync(Guid id)
    {
        if (!await _lossReadRepository.IsExist(id)) throw new Exception("Loss not found.");
        Loss loss = await _lossReadRepository.GetOneByCondition(c => c.Id == id && !c.IsDeleted, false)
                            ?? throw new Exception("Loss not found.");
        loss.IsDeleted = true;
        _lossWriteRepository.Update(loss);

        var result = await _lossWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Loss could not be soft deleted.");
        }
    }

    public async Task RestoreLossAsync(Guid id)
    {
        if (!await _lossReadRepository.IsExist(id)) throw new Exception("Loss not found.");
        Loss loss = await _lossReadRepository.GetOneByCondition(c => c.Id == id && c.IsDeleted, false)
                            ?? throw new Exception("Loss not found.");
        loss.IsDeleted = false;
        _lossWriteRepository.Update(loss);

        var result = await _lossWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Loss could not be restored.");
        }
    }

    public async Task<ICollection<LossGetDTO>> GetAllSoftDeletedLoss()
    {
        ICollection<Loss> losses = await _lossReadRepository.GetAllByCondition(c => c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<LossGetDTO>>(losses);
    }

    public async Task<ICollection<LossGetDTO>> GetAllActiveLossAsync()
    {
        ICollection<Loss> losses = await _lossReadRepository.GetAllByCondition(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<LossGetDTO>>(losses);
    }

    public async Task<LossGetDTO> GetByIdLossAsync(Guid id)
    {
        if (!await _lossReadRepository.IsExist(id)) throw new Exception("Loss not found.");
        Loss loss = await _lossReadRepository.GetByIdAsync(id) ?? throw new Exception("Loss not found.");
        return _mapper.Map<LossGetDTO>(loss);
    }
}