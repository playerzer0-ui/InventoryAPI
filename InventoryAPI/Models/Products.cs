namespace InventoryAPI.Models
{
	public class Products
	{
		public Guid Id { get; set; }

		public string ProductName { get; set; }

		public int Quantity { get; set; }

		public double Price { get; set; }

        public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    }
}
