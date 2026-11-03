using GastroMaster.DataAccessLayer.Models;
using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IOrderDal : IGenericDal<Order>
    {
        Task<List<Order>> GetOrderWithDetail();
        Task<Order> GetOrderWithDetailById(int id);
        Task<decimal> GetTodayRevenueAsync();
        Task<int> GetClosedOrderCountAsync();
        Task<int> GetOccupiedMenuTableCountAsync();
        Task<Order> GetOrderByMenuTable(int id);
        Task<List<DailyRevenueModel>> GetLastWeekDailyRevenueAsync();
    }
}
