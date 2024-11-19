namespace InventoryAPI.Models
{
	public class Products
	{
		public Guid productId { get; set; }

		public string productName { get; set; }

		public int quantity { get; set; }

		public double price { get; set; }
	}
}
