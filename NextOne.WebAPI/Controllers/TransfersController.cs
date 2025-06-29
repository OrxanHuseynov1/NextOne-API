using BusinessLayer.DTOs.Transfer;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NextOne.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,HeadSeller,Seller")]
public class TransfersController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransfersController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTransfer([FromBody] TransferPostDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _transferService.CreateTransferAsync(request);
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
    public async Task<IActionResult> DeleteTransfer(Guid id)
    {
        try
        {
            await _transferService.DeleteTransferAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message == "Transfer not found.")
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
    public async Task<IActionResult> SoftDeleteTransfer(Guid id)
    {
        try
        {
            await _transferService.SoftDeleteTransferAsync(id);
            return Ok(new { message = "Transfer soft-deleted successfully." });
        }
        catch (Exception ex) when (ex.Message == "Transfer not found.")
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
    public async Task<IActionResult> RestoreTransfer(Guid id)
    {
        try
        {
            await _transferService.RestoreTransferAsync(id);
            return Ok(new { message = "Transfer restored successfully." });
        }
        catch (Exception ex) when (ex.Message == "Transfer not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(ICollection<TransferGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActiveTransfers()
    {
        try
        {
            var transfers = await _transferService.GetAllActiveTransferAsync();
            return Ok(transfers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("soft-deleted")]
    [ProducesResponseType(typeof(ICollection<TransferGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSoftDeletedTransfers()
    {
        try
        {
            var transfers = await _transferService.GetAllSoftDeletedTransfer();
            return Ok(transfers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TransferGetDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTransferById(Guid id)
    {
        try
        {
            var transfer = await _transferService.GetByIdTransferAsync(id);
            return Ok(transfer);
        }
        catch (Exception ex) when (ex.Message == "Transfer not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("history/{companyId}")] 
    [ProducesResponseType(typeof(ICollection<TransferGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTransferHistory(Guid companyId)
    {
        var userCompanyIdClaim = User.FindFirst("companyId")?.Value;
        if (string.IsNullOrEmpty(userCompanyIdClaim) || !Guid.TryParse(userCompanyIdClaim, out var parsedUserCompanyId) || parsedUserCompanyId != companyId)
        {
            return Unauthorized("Sizin bu şirkətin məlumatlarına giriş icazəniz yoxdur.");
        }

        try
        {
            var transfers = await _transferService.GetTransferHistoryByCompanyIdAsync(companyId);
            return Ok(transfers);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }
}