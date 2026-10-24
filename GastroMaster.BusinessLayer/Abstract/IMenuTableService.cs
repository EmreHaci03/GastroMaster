
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IMenuTableService:IGenericService<MenuTable>
    {
        Task<decimal> TActiveMenuTableCount();
        Task<decimal> TMenuTableCount();
    }
}
