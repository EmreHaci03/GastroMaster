using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface ICashRegisterDal:IGenericDal<CashRegister>
    {
        Task<decimal> GetTotalAmountAsync();
    }
}
