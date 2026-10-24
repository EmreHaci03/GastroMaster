using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IOrderService:IGenericService<Order>
    {
        Task<List<Order>> TGetOrderWithDetail();
        Task<Order> TGetOrderWithDetailById(int id);
        Task<decimal> TGetTodayRevenueAsync();
    }
}
