﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using InventoryAPI.Dto;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class OrderProductsController : ControllerBase
    {
        private readonly InventoryAPIContext _context;

        public OrderProductsController(InventoryAPIContext context)
        {
            _context = context;
        }

        // GET: api/OrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProducts>>> GetOrderProduct()
        {
			int userType = GetUserType();
			if (userType == 0)
			{
				var orderProducts = await _context.OrderProduct.ToListAsync();

				var orderProductsDto = orderProducts.Select(oP => new OrderProductsDto
				{
					OrderId = oP.OrderId,
					ProductId = oP.ProductId,
					Quantity = oP.Quantity
					//supplier can't see the price 
				}).ToList();

				return Ok(orderProductsDto);
			}

			return await _context.OrderProduct.ToListAsync();
        }

        // GET: api/OrderProducts/5
        [HttpGet("o/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderProductsDto>>> GetOrderProductsByOrderId(Guid orderId)
        {
            int userType = GetUserType();
            var orderProducts = await _context.OrderProduct
                .Where(op => op.OrderId == orderId)
                .Select(op => new OrderProductsDto
                {
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = userType == 0 ? (double?)null : op.Price
                })
                .ToListAsync();

            if (orderProducts.Count == 0)
            {
                return NotFound($"No order products found for OrderId {orderId}");
            }

            return Ok(orderProducts);
        }

        [HttpGet("p/{productId}")]
        public async Task<ActionResult<IEnumerable<OrderProductsDto>>> GetOrderProductsByProductId(Guid productId)
        {
            int userType = GetUserType();
            var orderProducts = await _context.OrderProduct
                .Where(op => op.ProductId == productId)
                .Select(op => new OrderProductsDto
                {
                    OrderId = op.OrderId,
                    ProductId = op.ProductId,
                    Quantity = op.Quantity,
                    Price = userType == 0 ? (double?)null : op.Price
                })
                .ToListAsync();

            if (orderProducts.Count == 0)
            {
                return NotFound($"No order products found for ProductId {productId}");
            }

            return Ok(orderProducts);
        }

        // PUT: api/OrderProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProducts(Guid id, OrderProductsDto orderProductsDto)
        {
            int userType = GetUserType();
            // Check if the ID in the route matches the OrderId in the DTO
            if (id != orderProductsDto.OrderId)
            {
                return BadRequest("The ID in the URL does not match the Order ID in the request body.");
            }

            // Retrieve the existing OrderProducts entry
            var existingOrderProduct = await _context.OrderProduct
                .FirstOrDefaultAsync(op => op.OrderId == id && op.ProductId == orderProductsDto.ProductId);

            if (existingOrderProduct == null)
            {
                return NotFound($"No order-product mapping found with OrderId {id} and ProductId {orderProductsDto.ProductId}.");
            }

            // Update the fields from the DTO
            existingOrderProduct.Quantity = orderProductsDto.Quantity;

            if (userType > 0)
            {
                existingOrderProduct.Price = orderProductsDto.Price.Value;
            }

            // Mark the entity as modified
            _context.Entry(existingOrderProduct).State = EntityState.Modified;

            // Save changes
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductsExists(id))
                {
                    return NotFound($"OrderProduct with OrderId {id} no longer exists.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 to indicate success without content
        }


        // POST: api/OrderProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderProducts>> PostOrderProducts(OrderProductsDto dto)
        {
            var orderProducts = new OrderProducts();
            int userType = GetUserType();
            if(userType == 0)
            {
                orderProducts = new OrderProducts
                {
                    OrderId = dto.OrderId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    Price = 0
                };
            }
            else
            {
                orderProducts = new OrderProducts
                {
                    OrderId = dto.OrderId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    Price = (double)dto.Price
                };
            }

            _context.OrderProduct.Add(orderProducts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderProductsExists(orderProducts.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderProducts", new { id = orderProducts.OrderId }, orderProducts);
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("o/{orderId}, p/{productId}")]
        public async Task<IActionResult> DeleteOrderProducts(Guid orderId, Guid productId)
        {
            var orderProducts = await _context.OrderProduct.FindAsync(orderId, productId);
            if (orderProducts == null)
            {
                return NotFound();
            }

            _context.OrderProduct.Remove(orderProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProductsExists(Guid id)
        {
            return _context.OrderProduct.Any(e => e.OrderId == id);
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
