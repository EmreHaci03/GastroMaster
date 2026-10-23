using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IOrderDetailDal:IGenericDal<OrderDetail>
    {
        Task<List<OrderDetail>> OrderDetailWithDetail();
        Task<OrderDetail> OrderDetailWithDetailById(int id);
        Task<OrderDetail> OrderDetailWithDetailByProductId(int id);
    }
}
