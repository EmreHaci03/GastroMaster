using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IDiscountService:IGenericService<Discount>
    {
        Task<List<Discount>> TGetStatusTrueDiscountList();
        Task TChangeStatusToTrue(int id);
        Task TChangeStatusToFalse(int id);
    }
}
