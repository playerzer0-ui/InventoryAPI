using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Dto;
using InventoryAPI.Models;
using System.Text;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryAPIContext _context;

        public ProductsController(InventoryAPIContext context)
        {
            _context = context;
        }

        [HttpPut("UpdatePrices")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task UpdateProductPricesAsync()
        {
            var products = await _context.Product
                .Include(p => p.OrderProducts)
                .ToListAsync();

            foreach (var product in products)
            {
                if (product.OrderProducts.Any())
                {
                    product.Price = product.OrderProducts.Average(op => op.Price);
                }
                else
                {
                    product.Price = 0; // Default to 0 if no orders exist
                }
            }

            await _context.SaveChangesAsync(); // Save changes to the database
        }


        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(Guid id)
        {
            var products = await _context.Product.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(Guid id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("export")]
        [Produces("text/csv")]
        public async Task<IActionResult> ExportProducts()
        {
            int userType = GetUserType();
            if(userType == 0)
            {
                return Unauthorized("invalid user type");
            }

            //calculate average price and update database
            await UpdateProductPricesAsync();
            var products = await _context.Product.ToListAsync();

            // Map to DTO and exclude Price if userType is 0
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.ProductName,
                Quantity = p.Quantity,
                Price = userType == 0 ? (double?)null : p.Price // Exclude price for userType 0
            }).ToList();

            // Generate CSV content
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Id,Name,Quantity,Price");

            foreach (var product in productDtos)
            {
                csvBuilder.AppendLine($"{product.Id},{product.Name},{product.Quantity},{(product.Price.HasValue ? product.Price.Value.ToString("F2") : "")}");
            }

            var csvContent = csvBuilder.ToString();
            var csvBytes = Encoding.UTF8.GetBytes(csvContent);

            // Return as a file download
            return File(csvBytes, "text/csv", "Products.csv");
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.Product.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.Id }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(Guid id)
        {
            var products = await _context.Product.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Product.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        protected int GetUserType()
        {
            var userTypeClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value;
            if (userTypeClaim == null)
                return 0;

            //convert to int
            int userType = int.Parse(userTypeClaim);
            return userType;
        }
    }
}
