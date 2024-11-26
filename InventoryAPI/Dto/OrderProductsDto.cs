﻿namespace InventoryAPI.Dto
{
	public class OrderProductsDto
	{
		public Guid OrderId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
		public double? Price { get; set; }
	}
}
