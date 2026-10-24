using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IOrderDal : IGenericDal<Order>
    {
        Task<List<Order>> GetOrderWithDetail();
        Task<Order> GetOrderWithDetailById(int id);
        Task<decimal> GetTodayRevenueAsync();
    }
}
