

namespace GastroMaster.DtoLayer.DTOS.StatisticDtos
{
    public class PriceByCategoryDto
    {
        public string CategoryName { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
