
namespace GastroMaster.DtoLayer.DTOS.OrderDtos
{
    public class GetOrderByIdDto
    {
        public int OrderId { get; set; }
        public string TableNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
