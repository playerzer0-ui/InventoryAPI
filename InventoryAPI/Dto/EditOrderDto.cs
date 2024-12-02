namespace InventoryAPI.Dto
{
	public class EditOrderDto
	{
		public Guid Id { get; set; }

		public DateOnly OrderDate { get; set; }

		public string OrderType { get; set; }
	}
}
