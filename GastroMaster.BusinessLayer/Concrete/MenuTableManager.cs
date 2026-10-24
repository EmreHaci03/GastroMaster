using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class MenuTableManager : IMenuTableService
    {
        private readonly IMenuTableDal _menuTableDal;

        public MenuTableManager(IMenuTableDal menuTableDal)
        {
            _menuTableDal = menuTableDal;
        }

        public async Task<decimal> TActiveMenuTableCount()
        {
            return await _menuTableDal.ActiveMenuTableCount();
        }

        public async Task TDeleteAsync(MenuTable t)
        {
            await _menuTableDal.DeleteAsync(t);
        }

        public async Task<List<MenuTable>> TGetAllAsync()
        {
            return await _menuTableDal.GetAllAsync();
        }

        public async Task<MenuTable?> TGetByIdAsync(int id)
        {
            return await _menuTableDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(MenuTable t)
        {
            await _menuTableDal.InsertAsync(t);
        }

        public async Task<decimal> TMenuTableCount()
        {
            return await _menuTableDal.MenuTableCount();
        }

        public async Task TUpdateAsync(MenuTable t)
        {
            await _menuTableDal.UpdateAsync(t);
        }
    }
}
    