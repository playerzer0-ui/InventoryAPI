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
    [Authorize(Policy = "AdminOnly")]
    public class InvoicesController : ControllerBase
    {
        private readonly InventoryAPIContext _context;

        public InvoicesController(InventoryAPIContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoices>>> GetInvoice()
        {
            return await _context.Invoice.ToListAsync();
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoices>> GetInvoices(Guid id)
        {
            var invoices = await _context.Invoice.FindAsync(id);

            if (invoices == null)
            {
                return NotFound();
            }

            return invoices;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoices(Guid id, EditInvoiceDto editInvoiceDto)
        {
            // Validate that the route ID matches the DTO ID
            if (id != editInvoiceDto.Id)
            {
                return BadRequest("The ID in the URL does not match the Invoice ID in the request body.");
            }

            // Retrieve the existing invoice record
            var existingInvoice = await _context.Invoice.FindAsync(id);

            if (existingInvoice == null)
            {
                return NotFound($"Invoice with ID {id} not found.");
            }

            // Update fields from the DTO
            existingInvoice.InvoiceDate = editInvoiceDto.InvoiceDate;
            existingInvoice.OrderId = editInvoiceDto.OrderId;

            // Mark the entity as modified
            _context.Entry(existingInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoicesExists(id))
                {
                    return NotFound($"Invoice with ID {id} no longer exists.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 No Content to indicate success
        }

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoices>> PostInvoice(InvoiceDto dto)
        {
            var invoice = new Invoices
            {
                InvoiceDate = dto.InvoiceDate,
                OrderId = dto.OrderId
            };

            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoices(Guid id)
        {
            var invoices = await _context.Invoice.FindAsync(id);
            if (invoices == null)
            {
                return NotFound();
            }

            _context.Invoice.Remove(invoices);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoicesExists(Guid id)
        {
            return _context.Invoice.Any(e => e.Id == id);
        }
    }
}
