namespace InventoryAPI.Dto
{
    public class InvoiceProductDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice => Quantity * Price;
    }
}
