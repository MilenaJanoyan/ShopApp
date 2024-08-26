using ShopApp.Products.Enum;
using ShopApp.Products.Entity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Products.Service.Interface;
using ShopApp.Products.Repository.Interface;

namespace ShopApp.Products.Controller;

/// <summary>
/// Controller for managing products.
/// </summary>
//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;

    public ProductsController(
        IProductService productService,
        IProductRepository productRepository)
    {
        _productService = productService;
        _productRepository = productRepository;
    }

    //TODO: Add advanced search

    //TODO: Add get for all the products

    //TODO: Add users, login and other realted logic

    /// <summary>
    /// Retrieves a list of products.
    /// </summary>
    /// <param name="skip">Number of products to skip for pagination.</param>
    /// <param name="take">Number of products to take for pagination.</param>
    /// <param name="searchTerm">Term to search products by name.</param>
    /// <returns>A list of products.</returns>
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(int skip = 0, int take = 10, string searchTerm = "")
    {
        var products = await _productRepository.GetAllProductsAsync();
        products = _productService.SearchProducts(products, searchTerm);
        var paginatedProducts = products.Skip(skip).Take(take).ToList();
        return Ok(paginatedProducts);
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The product ID.</param>
    /// <returns>The product.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        product.Id = Guid.NewGuid();
        product.Status = ProductEnums.ProductStatus.InStock;
        product.CreatedDate = DateTime.UtcNow;
        await _productRepository.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="product">The updated product object.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        await _productRepository.UpdateProductAsync(product);
        return Ok(product);
    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        await _productRepository.DeleteProductAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Buys a specified quantity of a product.
    /// </summary>
    /// <param name="id">The ID of the product to buy.</param>
    /// <param name="quantity">The quantity to purchase.</param>
    /// <returns>ActionResult indicating success or failure.</returns>
    [HttpPost("{id}/buy")]
    public async Task<IActionResult> BuyProduct(Guid id, int quantity)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        if (product.Status == ProductEnums.ProductStatus.OutOfStock)
        {
            return BadRequest("Product is currently out of stock.");
        }

        if (quantity > product.StockQuantity)
        {
            return BadRequest($"Requested quantity ({quantity}) exceeds available stock ({product.StockQuantity}).");
        }

        bool purchaseSuccess = _productService.Buy(product.Id, quantity);

        if (!purchaseSuccess)
        {
            return BadRequest("Failed to purchase the product.");
        }

        return Ok("Product purchased successfully.");
    }
}
