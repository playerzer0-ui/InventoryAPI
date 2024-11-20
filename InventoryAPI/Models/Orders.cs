namespace InventoryAPI.Models
{
	public class Orders
	{
		public Guid Id { get; set; }

		public DateOnly orderDate {  get; set; }
		
		public Guid productId { get; set; }

		public int quantity { get; set; }

		public string orderType { get; set; }

        public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
        public Invoices? Invoice { get; set; }
    }
}
