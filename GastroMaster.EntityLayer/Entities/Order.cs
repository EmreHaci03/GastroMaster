namespace GastroMaster.EntityLayer.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public MenuTable MenuTable { get; set; }
        public int MenuTableId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date {  get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
