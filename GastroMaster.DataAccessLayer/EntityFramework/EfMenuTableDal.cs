using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfMenuTableDal : GenericRepository<MenuTable>, IMenuTableDal
    {
        private readonly GastroMasterContext _context;
        public EfMenuTableDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task<decimal> ActiveMenuTableCount()
        {
            return await _context.MenuTables.CountAsync(x => x.Status);
        }

        public async Task<decimal> MenuTableCount()
        {
            return await _context.MenuTables.CountAsync();
        }
    }
}
