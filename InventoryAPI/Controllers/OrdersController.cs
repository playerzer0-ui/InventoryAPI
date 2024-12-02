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
using Microsoft.AspNetCore.Authorization;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class OrdersController : ControllerBase
    {
        private readonly InventoryAPIContext _context;

        public OrdersController(InventoryAPIContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(Guid id)
        {
            var orders = await _context.Order.FindAsync(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(Guid id, EditOrderDto editOrderDto)
        {
            if (id != editOrderDto.Id)
            {
                return BadRequest();
            }

			//var existingProduct = await _context.Product.FindAsync(id);
			//if (existingProduct == null)
			//{
			//	return NotFound($"Product with ID {id} not found.");
			//}

			_context.Entry(editOrderDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(OrderDto dto)
        {
            var orders = new Orders
            {
                OrderDate = dto.OrderDate,
                OrderType = dto.OrderType
            };
            _context.Order.Add(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = orders.Id }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(Guid id)
        {
            var orders = await _context.Order.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Order.Remove(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersExists(Guid id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
