namespace InventoryAPI.Dto
{
    public class InvoiceDto
    {
        public DateOnly InvoiceDate { get; set; }
        public Guid OrderId { get; set; }

    }
}
