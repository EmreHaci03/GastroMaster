using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfDiscountDal : GenericRepository<Discount>, IDiscountDal
    {
        private readonly GastroMasterContext _context;
        public EfDiscountDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task ChangeStatusToFalse(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount != null)
            {
                discount.Status = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeStatusToTrue(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount != null)
            {
                discount.Status = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Discount>> GetStatusTrueDiscountList()
        {
            return await _context.Discounts.Where(x => x.Status).ToListAsync();
        }
    }
}
