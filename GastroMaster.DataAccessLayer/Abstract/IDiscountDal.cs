using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IDiscountDal : IGenericDal<Discount>
    {
        Task<List<Discount>> GetStatusTrueDiscountList();
        Task ChangeStatusToTrue(int id);
        Task ChangeStatusToFalse(int id);
    }
}
