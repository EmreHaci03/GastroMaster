namespace GastroMaster.DataAccessLayer.Models
{
    public class GetMenuTableWithOrderStatusModel
    {
        public int MenuTableId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool IsFull { get; set; }
        public string OrderStatus { get; set; }
    }
}
