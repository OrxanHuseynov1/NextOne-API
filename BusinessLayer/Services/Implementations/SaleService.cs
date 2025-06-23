using BusinessLayer.DTOs.Sale;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace BusinessLayer.Services.Implementations;

public class SaleService : ISaleService
{
    private readonly ISaleReadRepository _saleReadRepository;
    private readonly ISaleWriteRepository _saleWriteRepository;
    private readonly IMapper _mapper;

    public SaleService(ISaleReadRepository saleReadRepository, ISaleWriteRepository saleWriteRepository, IMapper mapper)
    {
        _saleReadRepository = saleReadRepository;
        _saleWriteRepository = saleWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateSaleAsync(SalePostDTO SalePostDTO)
    {
        Sale sale = _mapper.Map<Sale>(SalePostDTO);

        await _saleWriteRepository.CreateAsync(sale);
        var result = await _saleWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to create sale.");
        }
    }

    public async Task DeleteSaleAsync(Guid id)
    {
        if (!await _saleReadRepository.IsExist(id)) throw new Exception("Sale not found.");
        Sale sale = await _saleReadRepository.GetByIdAsync(id) ?? throw new Exception("Sale not found.");

        _saleWriteRepository.Delete(sale);

        var result = await _saleWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to delete sale.");
        }
    }

    public async Task SoftDeleteSaleAsync(Guid id)
    {
        if (!await _saleReadRepository.IsExist(id)) throw new Exception("Sale not found.");
        Sale sale = await _saleReadRepository.GetOneByCondition(s => s.Id == id && !s.IsDeleted, false)
                             ?? throw new Exception("Sale not found.");
        sale.IsDeleted = true;
        _saleWriteRepository.Update(sale);

        var result = await _saleWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to soft delete sale.");
        }
    }

    public async Task RestoreSaleAsync(Guid id)
    {
        if (!await _saleReadRepository.IsExist(id)) throw new Exception("Sale not found.");
        Sale sale = await _saleReadRepository.GetOneByCondition(s => s.Id == id && s.IsDeleted, false)
                             ?? throw new Exception("Sale not found.");
        sale.IsDeleted = false;
        _saleWriteRepository.Update(sale);

        var result = await _saleWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Failed to restore sale.");
        }
    }

    public async Task<ICollection<SaleGetDTO>> GetAllSoftDeletedSale()
    {
        ICollection<Sale> sales = await _saleReadRepository.GetAllByCondition(s => s.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<SaleGetDTO>>(sales);
    }

    public async Task<ICollection<SaleGetDTO>> GetAllActiveSaleAsync()
    {
        ICollection<Sale> sales = await _saleReadRepository.GetAllByCondition(s => !s.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<SaleGetDTO>>(sales);
    }

    public async Task<SaleGetDTO> GetByIdSaleAsync(Guid id)
    {
        if (!await _saleReadRepository.IsExist(id)) throw new Exception("Sale not found.");
        Sale sale = await _saleReadRepository.GetByIdAsync(id) ?? throw new Exception("Sale not found.");
        return _mapper.Map<SaleGetDTO>(sale);
    }
}