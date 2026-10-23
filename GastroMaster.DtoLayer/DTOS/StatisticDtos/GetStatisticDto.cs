
namespace GastroMaster.DtoLayer.DTOS.StatisticDtos
{
    public class GetStatisticDto
    {
        public int ProductCount { get; set; }
        public int ActiveProductCount { get; set; }
        public int PassiveProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int ActiveCategoryCount { get; set; }
        public int PassiveCategoryCount { get; set; }
    }
}
