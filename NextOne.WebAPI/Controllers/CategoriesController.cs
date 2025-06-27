using BusinessLayer.DTOs.Category;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace NextOne.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryPostDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _categoryService.CreateCategoryAsync(request);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryPutDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _categoryService.UpdateCategoryAsync(request);
            return Ok();
        }
        catch (Exception ex) when (ex.Message == "Category not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message == "Category not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPut("soft-delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SoftDeleteCategory(Guid id)
    {
        try
        {
            await _categoryService.SoftDeleteCategoryAsync(id);
            return Ok(new { message = "Category soft-deleted successfully." });
        }
        catch (Exception ex) when (ex.Message == "Category not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpPut("restore/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RestoreCategory(Guid id)
    {
        try
        {
            await _categoryService.RestoreCategoryAsync(id);
            return Ok(new { message = "Category restored successfully." });
        }
        catch (Exception ex) when (ex.Message == "Category not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(ICollection<CategoryGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActiveCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllActiveCategoryAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("soft-deleted")]
    [ProducesResponseType(typeof(ICollection<CategoryGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSoftDeletedCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllSoftDeletedCategory();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("active/byCompany/{companyId}")]
    [ProducesResponseType(typeof(ICollection<CategoryGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActiveCategoriesByCompanyId(Guid companyId)
    {
        try
        {
            var categories = await _categoryService.GetAllActiveCategoriesByCompanyIdAsync(companyId);
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("soft-deleted/byCompany/{companyId}")]
    [ProducesResponseType(typeof(ICollection<CategoryGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSoftDeletedCategoriesByCompanyId(Guid companyId)
    {
        try
        {
            var categories = await _categoryService.GetAllSoftDeletedCategoriesByCompanyIdAsync(companyId);
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("paged/{companyId}")]
    [ProducesResponseType(typeof(ICollection<CategoryGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPagedCategoriesByCompanyId(Guid companyId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and pageSize must be positive integers.");
        }
        try
        {
            var categories = await _categoryService.GetPagedCategoriesByCompanyIdAsync(companyId, page, pageSize, search);
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryGetDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        try
        {
            var category = await _categoryService.GetByIdCategoryAsync(id);
            return Ok(category);
        }
        catch (Exception ex) when (ex.Message == "Category not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }
}