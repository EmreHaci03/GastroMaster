
namespace GastroMaster.DtoLayer.DTOS.OrderDtos
{
    public class CreateOrderDto
    {
        public string TableNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
