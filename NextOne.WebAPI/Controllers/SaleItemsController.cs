using BusinessLayer.DTOs.SaleItem;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace NextOne.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,HeadSeller,Seller")]
public class SaleItemsController : ControllerBase
{
    private readonly ISaleItemService _saleItemService;

    public SaleItemsController(ISaleItemService saleItemService)
    {
        _saleItemService = saleItemService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateSaleItem([FromBody] SaleItemPostDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _saleItemService.CreateSaleItemAsync(request);
            return StatusCode(StatusCodes.Status201Created);
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
    public async Task<IActionResult> DeleteSaleItem(Guid id)
    {
        try
        {
            await _saleItemService.DeleteSaleItemAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message == "Sale item not found.")
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
    public async Task<IActionResult> SoftDeleteSaleItem(Guid id)
    {
        try
        {
            await _saleItemService.SoftDeleteSaleItemAsync(id);
            return Ok(new { message = "Sale item soft-deleted successfully." });
        }
        catch (Exception ex) when (ex.Message == "Sale item not found.")
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
    public async Task<IActionResult> RestoreSaleItem(Guid id)
    {
        try
        {
            await _saleItemService.RestoreSaleItemAsync(id);
            return Ok(new { message = "Sale item restored successfully." });
        }
        catch (Exception ex) when (ex.Message == "Sale item not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(ICollection<SaleItemGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActiveSaleItems()
    {
        try
        {
            var saleItems = await _saleItemService.GetAllActiveSaleItemAsync();
            return Ok(saleItems);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("soft-deleted")]
    [ProducesResponseType(typeof(ICollection<SaleItemGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSoftDeletedSaleItems()
    {
        try
        {
            var saleItems = await _saleItemService.GetAllSoftDeletedSaleItem();
            return Ok(saleItems);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SaleItemGetDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleItemById(Guid id)
    {
        try
        {
            var saleItem = await _saleItemService.GetByIdSaleItemAsync(id);
            return Ok(saleItem);
        }
        catch (Exception ex) when (ex.Message == "Sale item not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }
}