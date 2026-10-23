

namespace GastroMaster.DataAccessLayer.Models
{
    public class ProductCountByStatusModel
    {
        public bool ProductStatus { get; set; }
        public int ProductCount { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
