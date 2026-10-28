using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Models;
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

        public async Task<List<GetMenuTableWithOrderStatusModel>> GetMenuTableWithOrderStatus()
        {
            return await _context.MenuTables.Select(x => new GetMenuTableWithOrderStatusModel
            {
                MenuTableId = x.MenuTableId,
                Name = x.Name,
                Status = x.Status,
                IsFull = x.Orders.Any(o => o.Description != "Hesap Kapatıldı"),
                OrderStatus = x.Orders.OrderByDescending(o => o.Date).Select(o => o.Description).FirstOrDefault() ?? "Sipariş Yok"
            }).ToListAsync();
        }

        public async Task<decimal> MenuTableCount()
        {
            return await _context.MenuTables.CountAsync();
        }
    }
}
