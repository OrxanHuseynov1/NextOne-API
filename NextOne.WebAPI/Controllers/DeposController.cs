using BusinessLayer.DTOs.Depo;
using BusinessLayer.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NextOne.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,HeadSeller,Seller")]
public class DeposController : ControllerBase
{
    private readonly IDepoService _depoService;

    public DeposController(IDepoService depoService)
    {
        _depoService = depoService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDepo([FromBody] DepoPostDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _depoService.CreateDepoAsync(request);
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
    public async Task<IActionResult> UpdateDepo([FromBody] DepoPutDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _depoService.UpdateDepoAsync(request);
            return Ok();
        }
        catch (Exception ex) when (ex.Message == "Depo not found.")
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
    public async Task<IActionResult> DeleteDepo(Guid id)
    {
        try
        {
            await _depoService.DeleteDepoAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message == "Depo not found.")
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
    public async Task<IActionResult> SoftDeleteDepo(Guid id)
    {
        try
        {
            await _depoService.SoftDeleteDepoAsync(id);
            return Ok(new { message = "Depo soft-deleted successfully." });
        }
        catch (Exception ex) when (ex.Message == "Depo not found.")
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
    public async Task<IActionResult> RestoreDepo(Guid id)
    {
        try
        {
            await _depoService.RestoreDepoAsync(id);
            return Ok(new { message = "Depo restored successfully." });
        }
        catch (Exception ex) when (ex.Message == "Depo not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(ICollection<DepoGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActiveDepos()
    {
        try
        {
            var depos = await _depoService.GetAllActiveDepoAsync();
            return Ok(depos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("soft-deleted")]
    [ProducesResponseType(typeof(ICollection<DepoGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllSoftDeletedDepos()
    {
        try
        {
            var depos = await _depoService.GetAllSoftDeletedDepo();
            return Ok(depos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DepoGetDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDepoById(Guid id)
    {
        try
        {
            var depo = await _depoService.GetByIdDepoAsync(id);
            return Ok(depo);
        }
        catch (Exception ex) when (ex.Message == "Depo not found.")
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("count/{companyId}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDepoCountByCompanyId(Guid companyId)
    {
        var userCompanyIdClaim = User.FindFirst("companyId")?.Value;
        if (string.IsNullOrEmpty(userCompanyIdClaim) || !Guid.TryParse(userCompanyIdClaim, out var parsedUserCompanyId) || parsedUserCompanyId != companyId)
        {
            return Unauthorized("Sizin bu şirkətin məlumatlarına giriş icazəniz yoxdur.");
        }

        try
        {
            var count = await _depoService.GetActiveDepoCountByCompanyIdAsync(companyId);
            return Ok(count);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("by-company/{companyId}")]
    [ProducesResponseType(typeof(ICollection<DepoGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllDeposByCompanyId(Guid companyId)
    {
        var userCompanyIdClaim = User.FindFirst("companyId")?.Value;
        if (string.IsNullOrEmpty(userCompanyIdClaim) || !Guid.TryParse(userCompanyIdClaim, out var parsedUserCompanyId) || parsedUserCompanyId != companyId)
        {
            return Unauthorized("Sizin bu şirkətin məlumatlarına giriş icazəniz yoxdur.");
        }

        try
        {
            var depos = await _depoService.GetAllDeposByCompanyIdAsync(companyId);
            return Ok(depos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("paged/{companyId}")]
    [ProducesResponseType(typeof(IEnumerable<DepoGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPagedDeposByCompanyId(Guid companyId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var userCompanyIdClaim = User.FindFirst("companyId")?.Value;
        if (string.IsNullOrEmpty(userCompanyIdClaim) || !Guid.TryParse(userCompanyIdClaim, out var parsedUserCompanyId) || parsedUserCompanyId != companyId)
        {
            return Unauthorized("Sizin bu şirkətin məlumatlarına giriş icazəniz yoxdur.");
        }

        try
        {
            var depos = await _depoService.GetPagedDeposByCompanyIdAsync(companyId, page, pageSize);
            return Ok(depos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }

    [HttpGet("product-counts/{companyId}")]
    [ProducesResponseType(typeof(Dictionary<Guid, int>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDepoProductCounts(Guid companyId)
    {
        var userCompanyIdClaim = User.FindFirst("companyId")?.Value;
        if (string.IsNullOrEmpty(userCompanyIdClaim) || !Guid.TryParse(userCompanyIdClaim, out var parsedUserCompanyId) || parsedUserCompanyId != companyId)
        {
            return Unauthorized("Sizin bu şirkətin məlumatlarına giriş icazəniz yoxdur.");
        }

        try
        {
            var counts = await _depoService.GetDepoProductCountsByCompanyIdAsync(companyId);
            return Ok(counts);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
        }
    }
}