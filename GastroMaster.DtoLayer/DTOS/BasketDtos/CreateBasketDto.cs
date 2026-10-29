
namespace GastroMaster.DtoLayer.DTOS.BasketDtos
{
    public class CreateBasketDto
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int MenuTableId { get; set; }
    }
}
