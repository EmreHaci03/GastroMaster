using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfOrderDal : GenericRepository<Order>, IOrderDal
    {
        private readonly GastroMasterContext _context;
        public EfOrderDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderWithDetail()
        {
            return await _context.Orders.Include(x => x.MenuTable).ToListAsync();
        }

        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return await _context.Orders
                .Where(x => x.Description == "Hesap Kapatıldı" && x.Date >= today && x.Date < tomorrow)
                .SumAsync(x => x.TotalPrice);

        }

        public async Task<Order> GetOrderWithDetailById(int id)
        {
            return await _context.Orders.Include(x => x.MenuTable).Where(x => x.OrderId == id).FirstOrDefaultAsync();
        }
    }
}
