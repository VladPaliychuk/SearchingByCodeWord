using Microsoft.AspNetCore.Mvc;
using SBCW.BLL.DTOs.Requests;
using SBCW.BLL.Services.Interfaces;

namespace SBCW.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet("GetAllProductsAsync")]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            _logger.LogInformation($"Retrieved {products.Count()} products");
            return Ok(products);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all products.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting all products.");
        }
    }

    [HttpGet("GetProductByIdAsync {id}")]
    public async Task<IActionResult> GetProductByIdAsync(Guid id)   
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            _logger.LogInformation($"Returned product with id: {id}");
            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while getting product by id: {id}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting product by id");
        }
    }

    [HttpPost("CreateProductAsync")]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Product object is invalid");
                return BadRequest("Invalid model object");
            }
        
            var product = await _productService.CreateProductAsync(request);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while creating product", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating product");
        }
    }

    [HttpPut("UpdateProductAsync {id}")]
    public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] ProductRequest request)
    {
        try
        {
            var product = await _productService.UpdateProductAsync(id, request);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while updating product", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating product");
        }
    }

    [HttpDelete("DeleteProductByIdAsync {id}")]
    public async Task<IActionResult> DeleteProductByIdAsync(Guid id)
    {
        try
        {
            await _productService.DeleteProductByIdAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while deleting product", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting product");
        }
    }
}