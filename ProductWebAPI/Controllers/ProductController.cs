using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Models;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext productDbContext)
        {
                _context = productDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Products>> GetProducts()
        {

            return _context.Products;
        }

        [HttpGet("{productid:int}")]
        public async Task<ActionResult<Products>> GetProductById(int productid)
        {
            var product = await _context.Products.FindAsync(productid);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Products products)
        {
            if (products == null)
            {
                return BadRequest("Product is not Created");
            }
            await _context.Products.AddAsync(products);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(Products products)
        {
            _context.Products.Update(products);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{productid:int}")]
        public async Task<ActionResult<Products>> DeleteProduct(int productid)
        {
            var product = await _context.Products.FindAsync(productid);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
