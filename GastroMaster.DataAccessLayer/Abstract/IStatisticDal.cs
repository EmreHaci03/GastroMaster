using GastroMaster.DataAccessLayer.Models;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IStatisticDal
    {
        Task<GetStatisticModel> GetStatisticModelsAsync();
        Task<ProductCountByCategoryModel> ProductCountByCategoryAsync();

        Task<ProductCountByPriceRangeModel> ReceiveMostExpensiveProduct();
        Task<ProductCountByPriceRangeModel> ReceiveCheapestProduct();
        Task<decimal> GetAverageHamburgerPrice();
        Task<ResultOrderModel> GetOrderCountByStatus();
        Task<Order> GetLastOrderAsync();
    }
}