namespace InventoryAPI.Models
{
	public class Orders
	{
		public Guid orderId { get; set; }

		public DateOnly orderDate {  get; set; }
		
		public Guid productId { get; set; }

		public int quantity { get; set; }

		public string orderType { get; set; }
	}
}
