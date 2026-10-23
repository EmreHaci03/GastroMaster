
namespace GastroMaster.DtoLayer.DTOS.OrderDtos
{
    public class GetOrderWithProductByIdDto
    {
        public int OrderId { get; set; }
        public string TableNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; }
    }
}
