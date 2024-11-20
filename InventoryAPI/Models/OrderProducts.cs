namespace InventoryAPI.Models
{
	public class OrderProducts
	{
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }

    }
}
