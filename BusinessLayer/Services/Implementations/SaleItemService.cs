using BusinessLayer.DTOs.SaleItem;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace BusinessLayer.Services.Implementations;

public class SaleItemService : ISaleItemService
{
    private readonly ISaleItemReadRepository _saleItemReadRepository;
    private readonly ISaleItemWriteRepository _saleItemWriteRepository;
    private readonly IMapper _mapper;

    public SaleItemService(ISaleItemReadRepository saleItemReadRepository, ISaleItemWriteRepository saleItemWriteRepository, IMapper mapper)
    {
        _saleItemReadRepository = saleItemReadRepository;
        _saleItemWriteRepository = saleItemWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateSaleItemAsync(SaleItemPostDTO SaleItemPostDTO)
    {
        SaleItem saleItem = _mapper.Map<SaleItem>(SaleItemPostDTO);

        await _saleItemWriteRepository.CreateAsync(saleItem);
        var result = await _saleItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to create sale item.");
        }
    }

    public async Task DeleteSaleItemAsync(Guid id)
    {
        if (!await _saleItemReadRepository.IsExist(id)) throw new Exception("Sale item not found.");
        SaleItem saleItem = await _saleItemReadRepository.GetByIdAsync(id) ?? throw new Exception("Sale item not found.");

        _saleItemWriteRepository.Delete(saleItem);

        var result = await _saleItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to delete sale item.");
        }
    }

    public async Task SoftDeleteSaleItemAsync(Guid id)
    {
        if (!await _saleItemReadRepository.IsExist(id)) throw new Exception("Sale item not found.");
        SaleItem saleItem = await _saleItemReadRepository.GetOneByCondition(si => si.Id == id && !si.IsDeleted, false)
                             ?? throw new Exception("Sale item not found.");
        saleItem.IsDeleted = true;
        _saleItemWriteRepository.Update(saleItem);

        var result = await _saleItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to soft delete sale item.");
        }
    }

    public async Task RestoreSaleItemAsync(Guid id)
    {
        if (!await _saleItemReadRepository.IsExist(id)) throw new Exception("Sale item not found.");
        SaleItem saleItem = await _saleItemReadRepository.GetOneByCondition(si => si.Id == id && si.IsDeleted, false)
                             ?? throw new Exception("Sale item not found.");
        saleItem.IsDeleted = false;
        _saleItemWriteRepository.Update(saleItem);

        var result = await _saleItemWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to restore sale item.");
        }
    }

    public async Task<ICollection<SaleItemGetDTO>> GetAllSoftDeletedSaleItem()
    {
        ICollection<SaleItem> saleItems = await _saleItemReadRepository.GetAllByCondition(si => si.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<SaleItemGetDTO>>(saleItems);
    }

    public async Task<ICollection<SaleItemGetDTO>> GetAllActiveSaleItemAsync()
    {
        ICollection<SaleItem> saleItems = await _saleItemReadRepository.GetAllByCondition(si => !si.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<SaleItemGetDTO>>(saleItems);
    }

    public async Task<SaleItemGetDTO> GetByIdSaleItemAsync(Guid id)
    {
        if (!await _saleItemReadRepository.IsExist(id)) throw new Exception("Sale item not found.");
        SaleItem saleItem = await _saleItemReadRepository.GetByIdAsync(id) ?? throw new Exception("Sale item not found.");
        return _mapper.Map<SaleItemGetDTO>(saleItem);
    }
}