namespace GastroMaster.DtoLayer.DTOS.BasketDtos
{
    public class GetBasketWithDetailsByIdDto
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string MenuTable { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
