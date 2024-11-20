namespace InventoryAPI.Models
{
	public class Invoices
	{

		public Guid Id {  get; set; }

		public Guid orderId { get; set; }

		public DateOnly invoiceDate {  get; set; }

        public Guid OrderId { get; set; }
        public Orders Order { get; set; }

    }
}
