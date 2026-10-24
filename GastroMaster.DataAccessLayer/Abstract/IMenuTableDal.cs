using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IMenuTableDal:IGenericDal<MenuTable>
    {
        Task<decimal> ActiveMenuTableCount();
        Task<decimal> MenuTableCount();
    }
}
