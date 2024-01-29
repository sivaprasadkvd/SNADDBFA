using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SNADDBFA.Models;

namespace SNADDBFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SnaddbfaContext _context;

        public ProductController(SnaddbfaContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.ToList();

                if (products.Count == 0)
                {
                    return NotFound("Products not available.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

       //[HttpGet]

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"product details are not found with id {id}");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product Model)
        {
            try
            {
                _context.Add(Model);
                _context.SaveChanges();
                return Ok("Product Created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [HttpPut]

        public IActionResult Put(Product Model)
        {
            if(Model == null || Model.Id == 0)
            {
                if(Model == null)
                {
                    return BadRequest("Model data is invalid.");

                }
                else if(Model.Id == 0)
                {
                    return BadRequest($"Product Id {Model.Id} is invalid");
                }
            }

            var product = _context.Products.Find(Model.Id);
            if(product == null)
            {
                return NotFound($"Product not found with Id {Model.Id}");
            }
            try
            {
                product.ProductName = Model.ProductName;
                product.Price = Model.Price;
                product.Qty = Model.Qty;
                _context.SaveChanges();
                return Ok("Product details updated.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]

        public IActionResult Delete(int id)
        {

            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                {
                    return NotFound($"Product not found with id {id}");
                }

                _context.Products.Remove(product);  
                _context.SaveChanges();
                return Ok("Product details deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
