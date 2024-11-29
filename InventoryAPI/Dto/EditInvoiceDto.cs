namespace InventoryAPI.Dto
{
    public class EditInvoiceDto
    {
        public Guid Id { get; set; }
        public DateOnly InvoiceDate { get; set; }
        public Guid OrderId { get; set; }
    }
}
