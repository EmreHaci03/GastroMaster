using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfBasketDal : GenericRepository<Basket>, IBasketDal
    {
        private readonly GastroMasterContext _context;
        public EfBasketDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Basket>> GetBasketListWithDetail()
        {
            return await _context.Baskets.Include(x => x.Product).Include(x => x.MenuTable).ToListAsync();
        }

        public async Task<Basket?> GetBasketWithDetailById(int id)
        {
            return await _context.Baskets.Include(x => x.Product).Include(x => x.MenuTable).FirstOrDefaultAsync(x => x.BasketId == id);
        }

        public async Task<List<Basket>> GetBasketListByTableNumber(int id)
        {
            return await _context.Baskets.Include(x => x.Product).Include(x => x.MenuTable).Where(x => x.MenuTableId == id).ToListAsync();
        }

        public async Task<Basket?> GetBasketItemAsync(int menuTableId, int productId)
        {
            return await _context.Baskets.Include(x=>x.Product).Include(x=>x.MenuTable).FirstOrDefaultAsync(x => x.MenuTableId == menuTableId && x.ProductId == productId);
        }

    }
}