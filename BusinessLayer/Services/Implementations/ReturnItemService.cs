using AutoMapper;
using BusinessLayer.DTOs.ReturunItem;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class ReturnItemService : IReturnItemService
{
    private readonly IReturnItemReadRepository _returnItemReadRepository;
    private readonly IReturnItemWriteRepository _returnItemWriteRepository;
    private readonly IMapper _mapper;

    public ReturnItemService(IReturnItemReadRepository returnItemReadRepository, IReturnItemWriteRepository returnItemWriteRepository, IMapper mapper)
    {
        _returnItemReadRepository = returnItemReadRepository;
        _returnItemWriteRepository = returnItemWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateReturnItemAsync(ReturunItemPostDTO ReturnItemPostDTO)
    {
        ReturnItem returnItem = _mapper.Map<ReturnItem>(ReturnItemPostDTO);

        await _returnItemWriteRepository.CreateAsync(returnItem);
        var result = await _returnItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Return item could not be created.");
        }
    }

    public async Task DeleteReturnItemAsync(Guid id)
    {
        if (!await _returnItemReadRepository.IsExist(id)) throw new Exception("Return item not found.");
        ReturnItem returnItem = await _returnItemReadRepository.GetByIdAsync(id) ?? throw new Exception("Return item not found.");

        _returnItemWriteRepository.Delete(returnItem);

        var result = await _returnItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Return item could not be deleted.");
        }
    }

    public async Task SoftDeleteReturnItemAsync(Guid id)
    {
        if (!await _returnItemReadRepository.IsExist(id)) throw new Exception("Return item not found.");
        ReturnItem returnItem = await _returnItemReadRepository.GetOneByCondition(ri => ri.Id == id && !ri.IsDeleted, false)
                                  ?? throw new Exception("Return item not found.");
        returnItem.IsDeleted = true;
        _returnItemWriteRepository.Update(returnItem);

        var result = await _returnItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Return item could not be soft deleted.");
        }
    }

    public async Task RestoreReturnItemAsync(Guid id)
    {
        if (!await _returnItemReadRepository.IsExist(id)) throw new Exception("Return item not found.");
        ReturnItem returnItem = await _returnItemReadRepository.GetOneByCondition(ri => ri.Id == id && ri.IsDeleted, false)
                                  ?? throw new Exception("Return item not found.");
        returnItem.IsDeleted = false;
        _returnItemWriteRepository.Update(returnItem);

        var result = await _returnItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Return item could not be restored.");
        }
    }

    public async Task<ICollection<ReturnItemGetDTO>> GetAllSoftDeletedReturnItem()
    {
        ICollection<ReturnItem> returnItems = await _returnItemReadRepository.GetAllByCondition(ri => ri.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ReturnItemGetDTO>>(returnItems);
    }

    public async Task<ICollection<ReturnItemGetDTO>> GetAllActiveReturnItemAsync()
    {
        ICollection<ReturnItem> returnItems = await _returnItemReadRepository.GetAllByCondition(ri => !ri.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ReturnItemGetDTO>>(returnItems);
    }

    public async Task<ReturnItemGetDTO> GetByIdReturnItemAsync(Guid id)
    {
        if (!await _returnItemReadRepository.IsExist(id)) throw new Exception("Return item not found.");
        ReturnItem returnItem = await _returnItemReadRepository.GetByIdAsync(id) ?? throw new Exception("Return item not found.");
        return _mapper.Map<ReturnItemGetDTO>(returnItem);
    }
}