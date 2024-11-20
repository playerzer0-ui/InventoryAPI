namespace InventoryAPI.Models
{
	public class Products
	{
		public Guid Id { get; set; }

		public string productName { get; set; }

		public int quantity { get; set; }

		public double price { get; set; }

        public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    }
}
