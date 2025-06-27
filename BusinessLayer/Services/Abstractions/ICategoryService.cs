using BusinessLayer.DTOs.Category;

namespace BusinessLayer.Services.Abstractions;

public interface ICategoryService
{
    Task CreateCategoryAsync(CategoryPostDTO categoryPostDTO);
    Task DeleteCategoryAsync(Guid id);
    Task SoftDeleteCategoryAsync(Guid id);
    Task RestoreCategoryAsync(Guid id);
    Task UpdateCategoryAsync(CategoryPutDTO categoryPutDTO);
    Task<ICollection<CategoryGetDTO>> GetAllSoftDeletedCategory();
    Task<ICollection<CategoryGetDTO>> GetAllActiveCategoryAsync();
    Task<CategoryGetDTO> GetByIdCategoryAsync(Guid id);
    Task<ICollection<CategoryGetDTO>> GetAllActiveCategoriesByCompanyIdAsync(Guid companyId);
    Task<ICollection<CategoryGetDTO>> GetAllSoftDeletedCategoriesByCompanyIdAsync(Guid companyId);
    Task<ICollection<CategoryGetDTO>> GetPagedCategoriesByCompanyIdAsync(Guid companyId, int page, int pageSize, string? searchTerm = null);
}