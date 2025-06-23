using AutoMapper;
using BusinessLayer.DTOs.ProductInDepo;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class ProductInDepoService : IProductInDepoService
{
    private readonly IProductInDepoReadRepository _productInDepoReadRepository;
    private readonly IProductInDepoWriteRepository _productInDepoWriteRepository;
    private readonly IMapper _mapper;

    public ProductInDepoService(IProductInDepoReadRepository productInDepoReadRepository, IProductInDepoWriteRepository productInDepoWriteRepository, IMapper mapper)
    {
        _productInDepoReadRepository = productInDepoReadRepository;
        _productInDepoWriteRepository = productInDepoWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateProductInDepoAsync(ProductInDepoPostDTO ProductInDepoPostDTO)
    {
        ProductInDepo productInDepo = _mapper.Map<ProductInDepo>(ProductInDepoPostDTO);

        await _productInDepoWriteRepository.CreateAsync(productInDepo);
        var result = await _productInDepoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product in depo could not be created.");
        }
    }

    public async Task DeleteProductInDepoAsync(Guid id)
    {
        if (!await _productInDepoReadRepository.IsExist(id)) throw new Exception("Product in depo not found.");
        ProductInDepo productInDepo = await _productInDepoReadRepository.GetByIdAsync(id) ?? throw new Exception("Product in depo not found.");

        _productInDepoWriteRepository.Delete(productInDepo);

        var result = await _productInDepoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product in depo could not be deleted.");
        }
    }

    public async Task SoftDeleteProductInDepoAsync(Guid id)
    {
        if (!await _productInDepoReadRepository.IsExist(id)) throw new Exception("Product in depo not found.");
        ProductInDepo productInDepo = await _productInDepoReadRepository.GetOneByCondition(p => p.Id == id && !p.IsDeleted, false)
                                  ?? throw new Exception("Product in depo not found.");
        productInDepo.IsDeleted = true;
        _productInDepoWriteRepository.Update(productInDepo);

        var result = await _productInDepoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product in depo could not be soft deleted.");
        }
    }

    public async Task RestoreProductInDepoAsync(Guid id)
    {
        if (!await _productInDepoReadRepository.IsExist(id)) throw new Exception("Product in depo not found.");
        ProductInDepo productInDepo = await _productInDepoReadRepository.GetOneByCondition(p => p.Id == id && p.IsDeleted, false)
                                  ?? throw new Exception("Product in depo not found.");
        productInDepo.IsDeleted = false;
        _productInDepoWriteRepository.Update(productInDepo);

        var result = await _productInDepoWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product in depo could not be restored.");
        }
    }

    public async Task<ICollection<ProductInDepoGetDTO>> GetAllSoftDeletedProductInDepo()
    {
        ICollection<ProductInDepo> productsInDepo = await _productInDepoReadRepository.GetAllByCondition(p => p.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ProductInDepoGetDTO>>(productsInDepo);
    }

    public async Task<ICollection<ProductInDepoGetDTO>> GetAllActiveProductInDepoAsync()
    {
        ICollection<ProductInDepo> productsInDepo = await _productInDepoReadRepository.GetAllByCondition(p => !p.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ProductInDepoGetDTO>>(productsInDepo);
    }

    public async Task<ProductInDepoGetDTO> GetByIdProductInDepoAsync(Guid id)
    {
        if (!await _productInDepoReadRepository.IsExist(id)) throw new Exception("Product in depo not found.");
        ProductInDepo productInDepo = await _productInDepoReadRepository.GetByIdAsync(id) ?? throw new Exception("Product in depo not found.");
        return _mapper.Map<ProductInDepoGetDTO>(productInDepo);
    }
}