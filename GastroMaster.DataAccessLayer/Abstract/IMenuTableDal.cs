using GastroMaster.DataAccessLayer.Models;
using GastroMaster.EntityLayer.Entities;
namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IMenuTableDal:IGenericDal<MenuTable>
    {
        Task<List<GetMenuTableWithOrderStatusModel>> GetMenuTableWithOrderStatus();
        Task<decimal> ActiveMenuTableCount();
        Task<decimal> MenuTableCount();

    }
}
