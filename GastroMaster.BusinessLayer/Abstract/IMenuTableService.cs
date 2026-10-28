using GastroMaster.DataAccessLayer.Models;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IMenuTableService:IGenericService<MenuTable>
    {
        Task<List<GetMenuTableWithOrderStatusModel>> TGetMenuTableWithOrderStatus();
        Task<decimal> TActiveMenuTableCount();
        Task<decimal> TMenuTableCount();
    }
}
