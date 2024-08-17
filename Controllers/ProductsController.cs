using FreakyFashionAPI.Models;
using FreakyFashionAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreakyFashionAPI.Controllers
{
    [ApiController]
    [Route("api/products")] // Explicitly defining the route to match frontend calls
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _repository;

        public ProductsController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var productId = await _repository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = productId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            product.ProductId = id;
            await _repository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _repository.DeleteProduct(id);
            return NoContent();
        }
    }
}
