namespace InventoryAPI.Models
{
	public class OrderProducts
	{
		public Guid orderId {  get; set; }

		public Guid productId { get; set; }

		public int quantity { get; set; }

		public double price { get; set; }

	}
}
