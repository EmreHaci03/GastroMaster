namespace GastroMaster.DtoLayer.DTOS.OrderDetailDtos
{
    public class GetLastOrdersDto
    {
        public int OrderDetailId { get; set; }
        public string ProductName { get; set; }
        public string MenuTableName { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
