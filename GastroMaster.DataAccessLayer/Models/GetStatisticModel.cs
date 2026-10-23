namespace GastroMaster.DataAccessLayer.Models
{
    public class GetStatisticModel
    {
        public int ProductCount { get; set; }
        public int ActiveProductCount { get; set; }
        public int PassiveProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int ActiveCategoryCount { get; set; }
        public int PassiveCategoryCount { get; set; }
    }
}