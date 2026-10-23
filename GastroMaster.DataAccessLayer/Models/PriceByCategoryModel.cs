

namespace GastroMaster.DataAccessLayer.Models
{
    public class PriceByCategoryModel
    {
        public string CategoryName { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
