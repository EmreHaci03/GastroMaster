

namespace GastroMaster.DtoLayer.DTOS.OrderDtos
{
    public class GetOrderWithDetailByIdDto
    {
        public int OrderId { get; set; }
        public string MenuTableName { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
