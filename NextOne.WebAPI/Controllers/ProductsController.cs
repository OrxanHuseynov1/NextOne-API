    using BusinessLayer.DTOs.Product;
    using BusinessLayer.Services.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Security.Claims;

    namespace NextOne.WebAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductPostDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.CreateProductAsync(request);
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
        public async Task<IActionResult> UpdateProduct([FromBody] ProductPutDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.UpdateCustomerAsync(request);
                return Ok();
            }
            catch (Exception ex) when (ex.Message == "Product not found.")
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
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message == "Product not found.")
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
        public async Task<IActionResult> SoftDeleteProduct(Guid id)
        {
            try
            {
                await _productService.SoftDeleteProductAsync(id);
                return Ok(new { message = "Product soft-deleted successfully." });
            }
            catch (Exception ex) when (ex.Message == "Product not found.")
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
        public async Task<IActionResult> RestoreProduct(Guid id)
        {
            try
            {
                await _productService.RestoreProductAsync(id);
                return Ok(new { message = "Product restored successfully." });
            }
            catch (Exception ex) when (ex.Message == "Product not found.")
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("active")]
        [ProducesResponseType(typeof(ICollection<ProductGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllActiveProducts()
        {
            try
            {
                var products = await _productService.GetAllActiveProductAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("soft-deleted")]
        [ProducesResponseType(typeof(ICollection<ProductGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSoftDeletedProducts()
        {
            try
            {
                var products = await _productService.GetAllSoftDeletedProduct();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdProductAsync(id);
                return Ok(product);
            }
            catch (Exception ex) when (ex.Message == "Product not found.")
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
        public async Task<IActionResult> GetProductCountByCompanyId(Guid companyId)
        {
            var userCompanyIdClaim = User.FindFirst("companyId")?.Value;
            if (string.IsNullOrEmpty(userCompanyIdClaim) || !Guid.TryParse(userCompanyIdClaim, out var parsedUserCompanyId) || parsedUserCompanyId != companyId)
            {
                return Unauthorized("Sizin bu şirkətin məlumatlarına giriş icazəniz yoxdur.");
            }

            try
            {
                var count = await _productService.GetActiveProductCountByCompanyIdAsync(companyId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("total-quantity/{companyId}")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTotalProductQuantityByCompanyId(Guid companyId)
        {
            var userCompanyIdClaim = User.FindFirst("companyId")?.Value;
            if (string.IsNullOrEmpty(userCompanyIdClaim) || !Guid.TryParse(userCompanyIdClaim, out var parsedUserCompanyId) || parsedUserCompanyId != companyId)
            {
                return Unauthorized("Sizin bu şirkətin məlumatlarına giriş icazəniz yoxdur.");
            }

            try
            {
                var totalQuantity = await _productService.GetTotalActiveProductQuantityByCompanyIdAsync(companyId);
                return Ok(totalQuantity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }