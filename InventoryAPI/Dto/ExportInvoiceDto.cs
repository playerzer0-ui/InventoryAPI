namespace InventoryAPI.Dto
{
    public class ExportInvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public Guid OrderId { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public List<InvoiceProductDto> Products { get; set; } = new List<InvoiceProductDto>();
    }
}
