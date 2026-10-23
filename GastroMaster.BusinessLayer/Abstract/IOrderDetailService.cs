using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IOrderDetailService:IGenericService<OrderDetail>
    {
        Task<List<OrderDetail>> TOrderDetailWithDetail();
        Task<OrderDetail> TOrderDetailWithDetailById(int id);
        Task<OrderDetail> TOrderDetailWithDetailByProductId(int id);
    }
}
