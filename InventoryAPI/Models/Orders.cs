namespace InventoryAPI.Models
{
	public class Orders
	{
		public Guid Id { get; set; }

		public DateOnly OrderDate {  get; set; }

		public string OrderType { get; set; }

        public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
        public Invoices? Invoice { get; set; }
    }
}
