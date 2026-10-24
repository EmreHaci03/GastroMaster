namespace GastroMaster.DtoLayer.DTOS.OrderDtos
{
    public class ResultOrderDto
    {
        public int OrderId { get; set; }
        public string MenuTable { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
