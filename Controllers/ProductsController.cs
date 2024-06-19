using ShopApp.Models;
using ShopApp.Repositories.Interfaces;
using ShopApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static ShopApp.Enums.ProductEnums;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;

    public ProductsController(
        IProductRepository productRepository,
        IProductService productService)
    {
        _productRepository = productRepository;
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(int skip = 0, int take = 10, string searchTerm = "")
    {
        try
        {
            var products = await _productRepository.GetAllProductsAsync();

            products = _productService.SearchProducts(products, searchTerm);

            var paginatedProducts = products.Skip(skip).Take(take).ToList();
            return Ok(paginatedProducts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving products.");
        }
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        try
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving the product.");
        }
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        try
        {
            product.Id = Guid.NewGuid();
            product.Status = ProductStatus.InStock;
            product.CreatedDate = DateTime.Now;

            await _productRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while posting the product.");
        }
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, Product product)
    {
        try
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productRepository.UpdateProductAsync(product);
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the product.");
        }
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting the product.");
        }
    }
}
