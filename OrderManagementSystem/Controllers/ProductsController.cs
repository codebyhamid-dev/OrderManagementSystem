using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.DTOs;
using OrderManagementSystem.Models;
using OrderManagementSystem.Repository.ProductRepo;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _repository.CreateProductAsync(productDTO);
            if (product == null)
            {
                return BadRequest("Product could not be created.");
            }
            return Ok(product);
        }
        [HttpGet("AllProducts")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllProductsAsync();
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("GetProduct/By/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Invalid product ID.");
            }
            var product = await _repository.GetProductByIdAsync(Id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }
        [HttpPut("UpdateProduct/Id/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repository.UpdateProductAsync(id, productDTO);
            if (result == null)
            {
                return NotFound("Product not found or update failed.");
            }
            return Ok(result);
        }
        [HttpDelete("DeleteProduct/Id/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid product ID.");
            }
            var result = await _repository.DeleteProductAsync(id);
            if (result == "Product deleted successfully.")
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("SearchProductByName")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return BadRequest("Search term is required.");
            }
            var products = await _repository.SearchProductsAsync(term);
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found matching the search term.");
            }
            return Ok(products);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetProductCount()
        {
            var count = await _repository.GetProductCountAsync();
            return Ok($"Total Number of Products: {count}");
        }

    }
}
