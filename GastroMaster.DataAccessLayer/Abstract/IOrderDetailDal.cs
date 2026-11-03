using GastroMaster.DataAccessLayer.Models;
using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IOrderDetailDal:IGenericDal<OrderDetail>
    {
        Task<List<OrderDetail>> OrderDetailWithDetail();
        Task<OrderDetail> OrderDetailWithDetailById(int id);
        Task<List<GetLastOrderModel>> GetLastOrders();
        Task<OrderDetail> OrderDetailWithDetailByProductId(int id);
        Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId);
        Task<List<TopSellingProductModels>> GetTopSellingProductsAsync();
    }
}
