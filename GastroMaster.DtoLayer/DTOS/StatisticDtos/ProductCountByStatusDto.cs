
namespace GastroMaster.DtoLayer.DTOS.StatisticDtos
{
    public class ProductCountByStatusDto
    {
        public string StatusName { get; set; }
        public int ProductCount { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
