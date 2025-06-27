using AutoMapper;
using BusinessLayer.DTOs.Product;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using DAL.SqlServer.Repositories.Implementations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IMapper _mapper;
    private readonly IProductInDepoReadRepository _productInDepoReadRepository;

    public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper,IProductInDepoReadRepository productInDepoReadRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
        _mapper = mapper;
        _productInDepoReadRepository = productInDepoReadRepository;
    }

    public async Task CreateProductAsync(ProductPostDTO ProductPostDTO)
    {
        Product product = _mapper.Map<Product>(ProductPostDTO);

        await _productWriteRepository.CreateAsync(product);
        var result = await _productWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product could not be created.");
        }
    }

    public async Task DeleteProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) throw new Exception("Product not found.");
        Product product = await _productReadRepository.GetByIdAsync(id) ?? throw new Exception("Product not found.");

        _productWriteRepository.Delete(product);

        var result = await _productWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product could not be deleted.");
        }
    }

    public async Task SoftDeleteProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) throw new Exception("Product not found.");
        Product product = await _productReadRepository.GetOneByCondition(p => p.Id == id && !p.IsDeleted, false)
                                ?? throw new Exception("Product not found.");
        product.IsDeleted = true;
        _productWriteRepository.Update(product);

        var result = await _productWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product could not be soft deleted.");
        }
    }

    public async Task RestoreProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) throw new Exception("Product not found.");
        Product product = await _productReadRepository.GetOneByCondition(p => p.Id == id && p.IsDeleted, false)
                                ?? throw new Exception("Product not found.");
        product.IsDeleted = false;
        _productWriteRepository.Update(product);

        var result = await _productWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product could not be restored.");
        }
    }

    public async Task UpdateCustomerAsync(ProductPutDTO productPutDTO)
    {
        if (!await _productReadRepository.IsExist(productPutDTO.Id)) throw new Exception("Product not found.");

        Product productToUpdate = await _productReadRepository.GetByIdAsync(productPutDTO.Id)
                                    ?? throw new Exception("Product not found.");

        _mapper.Map(productPutDTO, productToUpdate);
        _productWriteRepository.Update(productToUpdate);

        var result = await _productWriteRepository.SaveAsync();

        if (result == 0)
        {
            throw new Exception("Product could not be updated.");
        }
    }

    public async Task<ICollection<ProductGetDTO>> GetAllSoftDeletedProduct()
    {
        ICollection<Product> products = await _productReadRepository.GetAllByCondition(p => p.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ProductGetDTO>>(products);
    }

    public async Task<ICollection<ProductGetDTO>> GetAllActiveProductAsync()
    {
        ICollection<Product> products = await _productReadRepository.GetAllByCondition(p => !p.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<ProductGetDTO>>(products);
    }

    public async Task<ProductGetDTO> GetByIdProductAsync(Guid id)
    {
        if (!await _productReadRepository.IsExist(id)) throw new Exception("Product not found.");
        Product product = await _productReadRepository.GetByIdAsync(id) ?? throw new Exception("Product not found.");
        return _mapper.Map<ProductGetDTO>(product);
    }

    public async Task<int> GetActiveProductCountByCompanyIdAsync(Guid companyId)
    {
        return await _productReadRepository.GetAllByCondition(p => p.CompanyId == companyId && !p.IsDeleted).CountAsync();
    }

    public async Task<decimal> GetTotalActiveProductQuantityByCompanyIdAsync(Guid companyId)
    {
        return await _productInDepoReadRepository.GetAllByCondition(pid => pid.CompanyId == companyId && !pid.IsDeleted)
                                                  .SumAsync(pid => pid.Quantity);
    }
}