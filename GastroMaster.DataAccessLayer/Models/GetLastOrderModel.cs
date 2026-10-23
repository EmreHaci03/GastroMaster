namespace GastroMaster.DataAccessLayer.Models
{
    public class GetLastOrderModel
    {
        public int OrderId { get; set; }
        public string TableNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
