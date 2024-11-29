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
using Microsoft.AspNetCore.Authorization;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]

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
			int userType = GetUserType();
			if (userType == 0)
			{
				var products = await _context.Product.ToListAsync();

				var productDtos = products.Select(p => new ProductDto
				{
					Id = p.Id,
					Name = p.ProductName,
					Quantity = p.Quantity
					//supplier can't see the price 
				}).ToList(); 

                return Ok(productDtos);
			}

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

			int userType = GetUserType();
			if (userType == 0)
            {
                var productDtos = new ProductDto
			{
				Id = products.Id,
				Name = products.ProductName,
				Quantity = products.Quantity
				//supplier can't see the price 
			};
				return Ok(productDtos);
			}

			return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(Guid id, ProductDto productDto)
        {
            int userType = GetUserType();
            // Check if the ID in the route matches the ID in the DTO
            if (id != productDto.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            // Retrieve the product from the database
            var existingProduct = await _context.Product.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            // Map the fields from the DTO to the existing product entity
            existingProduct.ProductName = productDto.Name;
            existingProduct.Quantity = productDto.Quantity;

            if (userType == 1)
            {
                existingProduct.Price = productDto.Price.Value;
            }

            // Mark the product as modified
            _context.Entry(existingProduct).State = EntityState.Modified;

            // Save changes
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound($"Product with ID {id} not found after the update attempt.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 status code to indicate success
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
        public async Task<ActionResult<Products>> PostProducts(EditProductsDto editProducts)
        {
            int userType = GetUserType();
            if (userType == 0)
            {
				var editSupplierProductsDto = new Products
				{
					ProductName = editProducts.Name,
					Quantity = editProducts.Quantity,
					//Supplier won't see the price
				};

				_context.Product.Add(editSupplierProductsDto);
				await _context.SaveChangesAsync();

				return CreatedAtAction("GetProducts", new { id = editSupplierProductsDto.Id }, editSupplierProductsDto);
            }
            else
            {
             var editProductsDto = new Products
            {
                ProductName = editProducts.Name,
                Quantity = editProducts.Quantity,
                Price = editProducts.Price
            };

            _context.Product.Add(editProductsDto);
            await _context.SaveChangesAsync();

		    return CreatedAtAction("GetProducts", new { id = editProductsDto.Id }, editProductsDto);
            }
           
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
