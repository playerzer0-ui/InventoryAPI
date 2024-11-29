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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

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

        [HttpGet("export-invoice")]
        public async Task<IActionResult> ExportInvoiceToPdf(Guid invoiceId)
        {
            // Fetch the invoice along with associated data
            var invoice = await _context.Invoice
                .Include(i => i.Order)
                .ThenInclude(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);

            if (invoice == null)
            {
                return NotFound($"Invoice with ID {invoiceId} not found.");
            }

            // Map to DTO
            var invoiceDto = new ExportInvoiceDto
            {
                InvoiceId = invoice.Id,
                OrderId = invoice.OrderId,
                InvoiceDate = invoice.InvoiceDate,
                Products = invoice.Order.OrderProducts.Select(op => new InvoiceProductDto
                {
                    ProductName = op.Product.ProductName,
                    Quantity = op.Quantity,
                    Price = op.Price
                }).ToList()
            };

            // Generate PDF
            var pdfBytes = GenerateInvoicePdf(invoiceDto);

            // Return as a file download
            return File(pdfBytes, "application/pdf", $"Invoice_{invoiceDto.InvoiceId}.pdf");
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

        private byte[] GenerateInvoicePdf(ExportInvoiceDto invoiceDto)
        {
            using var memoryStream = new MemoryStream();

            // Create the PDF writer and document
            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            // Invoice Header
            document.Add(new Paragraph($"Invoice ID: {invoiceDto.InvoiceId}"));
            document.Add(new Paragraph($"Order ID: {invoiceDto.OrderId}"));
            document.Add(new Paragraph($"Invoice Date: {invoiceDto.InvoiceDate:yyyy-MM-dd}"));
            document.Add(new Paragraph(" ")); // Add space

            // Products Table
            var table = new Table(4); // 4 columns
            table.AddHeaderCell("Product Name");
            table.AddHeaderCell("Quantity");
            table.AddHeaderCell("Price");
            table.AddHeaderCell("Total");

            foreach (var product in invoiceDto.Products)
            {
                table.AddCell(product.ProductName);
                table.AddCell(product.Quantity.ToString());
                table.AddCell(product.Price.ToString("F2"));
                table.AddCell(product.TotalPrice.ToString("F2"));
            }

            document.Add(table);

            // Footer
            var totalAmount = invoiceDto.Products.Sum(p => p.TotalPrice);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph($"Total Amount: {totalAmount:F2}"));

            // Close the document
            document.Close();

            return memoryStream.ToArray();
        }



        private bool InvoicesExists(Guid id)
        {
            return _context.Invoice.Any(e => e.Id == id);
        }
    }
}
