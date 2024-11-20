namespace InventoryAPI.Models
{
	public class Invoices
	{

		public Guid Id {  get; set; }

		public Guid OrderId { get; set; }

		public DateOnly InvoiceDate {  get; set; }

        public Orders Order { get; set; }

    }
}
