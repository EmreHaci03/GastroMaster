using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Models;
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

        public async Task<List<GetLastOrderModel>> GetLastOrders()
        {
            return await _context.OrderDetails.OrderByDescending(x => x.OrderDetailId).Take(5).Select(x => new GetLastOrderModel
            {
                OrderDetailId = x.OrderDetailId,
                ProductName = x.Product.ProductName,
                MenuTableName = x.Order.MenuTable.Name,
                TotalPrice = x.TotalPrice,
                Description = x.Order.Description,
                Date = x.Order.Date
            }).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            return await _context.OrderDetails.Include(x => x.Product).ThenInclude(x => x.Category).Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<List<TopSellingProductModels>> GetTopSellingProductsAsync()
        {
            return await _context.OrderDetails.Include(x=>x.Product).ThenInclude(x=>x.Category).GroupBy(x => new {x.Product.ProductName,x.Product.Category.CategoryName}).Select(x => new TopSellingProductModels
            {
                SoldCount = x.Sum(o => o.Count),
                CategoryName=x.Key.CategoryName,
                ProductName = x.Key.ProductName,
            }).OrderByDescending(x => x.SoldCount)
            .Take(10)
            .ToListAsync();
        }

        public async Task<List<OrderDetail>> OrderDetailWithDetail()
        {
            return await _context.OrderDetails.Include(x => x.Product).Include(x => x.Order).ThenInclude(x=>x.MenuTable).ToListAsync();
        }

        public async Task<OrderDetail> OrderDetailWithDetailById(int id)
        {
            return await _context.OrderDetails.Include(x => x.Product).Include(x => x.Order).ThenInclude(x => x.MenuTable).FirstOrDefaultAsync(x=>x.OrderDetailId==id);
        }

        public async  Task<OrderDetail> OrderDetailWithDetailByProductId(int id)
        {
            return await _context.OrderDetails.Include(x => x.Product).Include(x => x.Order).ThenInclude(x => x.MenuTable).FirstOrDefaultAsync(x => x.ProductId == id);
        }
    }
}
