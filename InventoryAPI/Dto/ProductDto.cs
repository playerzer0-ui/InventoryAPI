﻿namespace InventoryAPI.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }

    }
}
