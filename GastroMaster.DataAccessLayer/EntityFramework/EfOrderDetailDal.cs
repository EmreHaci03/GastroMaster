using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfOrderDetailDal : GenericRepository<OrderDetail>, IOrderDetailDal
    {
        private readonly GastroMasterContext _context;
        public EfOrderDetailDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<OrderDetail>> OrderDetailWithDetail()
        {
            return await _context.OrderDetails.Include(x => x.Product).Include(x => x.Order).ToListAsync();
        }

        public async Task<OrderDetail> OrderDetailWithDetailById(int id)
        {
            return await _context.OrderDetails.Include(x => x.Product).Include(x => x.Order).FirstOrDefaultAsync(x=>x.OrderDetailId==id);
        }

        public async  Task<OrderDetail> OrderDetailWithDetailByProductId(int id)
        {
            return await _context.OrderDetails.Include(x => x.Product).Include(x => x.Order).FirstOrDefaultAsync(x => x.ProductId == id);
        }
    }
}
