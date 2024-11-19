namespace InventoryAPI.Models
{
	public class Invoices
	{

		public Guid invoiceId {  get; set; }

		public Guid orderId { get; set; }

		public DateOnly invoiceDate {  get; set; }

	}
}
