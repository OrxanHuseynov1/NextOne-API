using AutoMapper;
using BusinessLayer.DTOs.Category;
using BusinessLayer.Services.Abstractions;
using DAL.SqlServer.Repositories.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLayer.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _categoryReadRepository;
    private readonly ICategoryWriteRepository _categoryWriteRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryService(ICategoryReadRepository categoryReadRepository,
                           ICategoryWriteRepository categoryWriteRepository,
                           IMapper mapper,
                           IHttpContextAccessor httpContextAccessor)
    {
        _categoryReadRepository = categoryReadRepository;
        _categoryWriteRepository = categoryWriteRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateCategoryAsync(CategoryPostDTO categoryPostDTO)
    {
        Category category = _mapper.Map<Category>(categoryPostDTO);
        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        category.CreatedBy = currentUserId ?? "System";
        category.CreatedAt = DateTime.UtcNow.AddHours(4);

        await _categoryWriteRepository.CreateAsync(category);
        var result = await _categoryWriteRepository.SaveAsync();
        if (result == 0) throw new Exception("Category could not be created.");
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) throw new Exception("Category not found.");
        Category category = await _categoryReadRepository.GetByIdAsync(id) ?? throw new Exception("Category not found.");

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        category.DeletedBy = currentUserId ?? "System";
        category.DeletedAt = DateTime.UtcNow.AddHours(4);

        _categoryWriteRepository.Delete(category);
        var result = await _categoryWriteRepository.SaveAsync();
        if (result == 0) throw new Exception("Category could not be deleted.");
    }

    public async Task SoftDeleteCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) throw new Exception("Category not found.");
        Category category = await _categoryReadRepository.GetOneByCondition(c => c.Id == id && !c.IsDeleted, false)
                             ?? throw new Exception("Category not found.");

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        category.IsDeleted = true;
        category.LastModifiedBy = currentUserId ?? "System";
        category.LastModifiedAt = DateTime.UtcNow.AddHours(4);

        _categoryWriteRepository.Update(category);
        var result = await _categoryWriteRepository.SaveAsync();
        if (result == 0) throw new Exception("Category could not be soft deleted.");
    }

    public async Task RestoreCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) throw new Exception("Category not found.");
        Category category = await _categoryReadRepository.GetOneByCondition(c => c.Id == id && c.IsDeleted, false)
                             ?? throw new Exception("Category not found.");

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        category.IsDeleted = false;
        category.LastModifiedBy = currentUserId ?? "System";
        category.LastModifiedAt = DateTime.UtcNow.AddHours(4);

        _categoryWriteRepository.Update(category);
        var result = await _categoryWriteRepository.SaveAsync();
        if (result == 0) throw new Exception("Category could not be restored.");
    }

    public async Task UpdateCategoryAsync(CategoryPutDTO categoryPutDTO)
    {
        if (!await _categoryReadRepository.IsExist(categoryPutDTO.Id)) throw new Exception("Category not found.");
        Category categoryToUpdate = await _categoryReadRepository.GetByIdAsync(categoryPutDTO.Id)
                                     ?? throw new Exception("Category not found.");
        _mapper.Map(categoryPutDTO, categoryToUpdate);

        var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        categoryToUpdate.LastModifiedBy = currentUserId ?? "System";
        categoryToUpdate.LastModifiedAt = DateTime.UtcNow.AddHours(4);

        _categoryWriteRepository.Update(categoryToUpdate);
        var result = await _categoryWriteRepository.SaveAsync();
        if (result == 0) throw new Exception("Category could not be updated.");
    }

    public async Task<ICollection<CategoryGetDTO>> GetAllSoftDeletedCategory()
    {
        ICollection<Category> categories = await _categoryReadRepository.GetAllByCondition(c => c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<CategoryGetDTO>>(categories);
    }

    public async Task<ICollection<CategoryGetDTO>> GetAllActiveCategoryAsync()
    {
        ICollection<Category> categories = await _categoryReadRepository.GetAllByCondition(c => !c.IsDeleted).ToListAsync();
        return _mapper.Map<ICollection<CategoryGetDTO>>(categories);
    }

    public async Task<CategoryGetDTO> GetByIdCategoryAsync(Guid id)
    {
        if (!await _categoryReadRepository.IsExist(id)) throw new Exception("Category not found.");
        Category category = await _categoryReadRepository.GetByIdAsync(id) ?? throw new Exception("Category not found.");
        return _mapper.Map<CategoryGetDTO>(category);
    }

    public async Task<ICollection<CategoryGetDTO>> GetAllActiveCategoriesByCompanyIdAsync(Guid companyId)
    {
        ICollection<Category> categories = await _categoryReadRepository
            .GetAllByCondition(c => !c.IsDeleted && c.CompanyId == companyId, false)
            .Include(c => c.Products)
            .Include(c => c.Company)
            .ToListAsync();
        return _mapper.Map<ICollection<CategoryGetDTO>>(categories);
    }

    public async Task<ICollection<CategoryGetDTO>> GetAllSoftDeletedCategoriesByCompanyIdAsync(Guid companyId)
    {
        ICollection<Category> categories = await _categoryReadRepository
            .GetAllByCondition(c => c.IsDeleted && c.CompanyId == companyId, false)
            .Include(c => c.Products)
            .Include(c => c.Company)
            .ToListAsync();
        return _mapper.Map<ICollection<CategoryGetDTO>>(categories);
    }

    public async Task<ICollection<CategoryGetDTO>> GetPagedCategoriesByCompanyIdAsync(Guid companyId, int page, int pageSize, string? searchTerm = null)
    {
        var baseQuery = _categoryReadRepository
            .GetAllByCondition(c => c.CompanyId == companyId && !c.IsDeleted, false)
            .Include(c => c.Products)
            .Include(c => c.Company);

        IQueryable<Domain.Entities.Category> filteredQuery = baseQuery;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var lowerSearchTerm = searchTerm.ToLower();

            filteredQuery = filteredQuery.Where(c => c.Name != null && c.Name.ToLower().Contains(lowerSearchTerm));
        }

        var categories = await filteredQuery
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<ICollection<CategoryGetDTO>>(categories);
    }
}