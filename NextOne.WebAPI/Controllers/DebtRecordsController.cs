using BusinessLayer.DTOs.DebtRecord;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextOne.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DebtRecordsController : ControllerBase
{
    private readonly IDebtRecordService _debtRecordService;

    public DebtRecordsController(IDebtRecordService debtRecordService)
    {
        _debtRecordService = debtRecordService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDebtRecord([FromBody] DebtRecordPostDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _debtRecordService.CreateDebtRecordAsync(request);
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
    public async Task<IActionResult> DeleteDebtRecord(Guid id)
    {
        try
        {
            await _debtRecordService.DeleteDebtRecordAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message == "Debt record not found.")
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
    public async Task<IActionResult> SoftDeleteDebtRecord(Guid id)
    {
        try
        {
            await _debtRecordService.SoftDeleteDebtRecordAsync(id);
            return Ok(new { message = "Debt record soft-deleted successfully." });
        }
        catch (Exception ex) when (ex.Message == "Debt record not found.")
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
    public async Task<IActionResult> RestoreDebtRecord(Guid id)
    {
        try
        {
            await _debtRecordService.RestoreDebtRecordAsync(id);
            return Ok(new { message = "Debt record restored successfully." });
        }
        catch (Exception ex) when (ex.Message == "Debt record not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(ICollection<DebtRecordGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActiveDebtRecords()
    {
        try
        {
            var debtRecords = await _debtRecordService.GetAllActiveDebtRecordAsync();
            return Ok(debtRecords);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("soft-deleted")]
    [ProducesResponseType(typeof(ICollection<DebtRecordGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSoftDeletedDebtRecords()
    {
        try
        {
            var debtRecords = await _debtRecordService.GetAllSoftDeletedDebtRecord();
            return Ok(debtRecords);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DebtRecordGetDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDebtRecordById(Guid id)
    {
        try
        {
            var debtRecord = await _debtRecordService.GetByIdDebtRecordAsync(id);
            return Ok(debtRecord);
        }
        catch (Exception ex) when (ex.Message == "Debt record not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }
}