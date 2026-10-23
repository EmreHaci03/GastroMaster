namespace GastroMaster.DtoLayer.DTOS.OrderDetailDtos
{
    public class GetOrderDetailWithDetailDto
    {
        public int OrderDetailId { get; set; }
        public string Product { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Order { get; set; }
    }
}
