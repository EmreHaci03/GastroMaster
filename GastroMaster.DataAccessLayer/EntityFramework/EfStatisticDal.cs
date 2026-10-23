using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfStatisticDal : IStatisticDal
    {
        private readonly GastroMasterContext _context;

        public EfStatisticDal(GastroMasterContext context)
        {
            _context = context;
        }
        public async Task<GetStatisticModel> GetStatisticModelsAsync()
        {
            return new GetStatisticModel
            {
                ProductCount = await _context.Products.CountAsync(),
                ActiveProductCount = await _context.Products.CountAsync(x => x.ProductStatus),
                PassiveProductCount = await _context.Products.CountAsync(x => !x.ProductStatus),
                CategoryCount = await _context.Categories.CountAsync(),
                ActiveCategoryCount = await _context.Categories.CountAsync(x => x.CategoryStatus),
                PassiveCategoryCount = await _context.Categories.CountAsync(x => !x.CategoryStatus)
            };
        }

        public async Task<ProductCountByCategoryModel> ProductCountByCategoryAsync()
        {
          return await _context.Products.GroupBy(x=>x.Category.CategoryName).Select(x=> new ProductCountByCategoryModel
          {
              CategoryName=x.Key,
              ProductCount=x.Count()
          }).OrderByDescending(x=>x.ProductCount).FirstOrDefaultAsync();
        }
        public async Task<ProductCountByPriceRangeModel> ReceiveMostExpensiveProduct()
        {
            return await _context.Products.OrderByDescending(x => x.Price).Select(x => new ProductCountByPriceRangeModel
            {
                CategoryName = x.Category.CategoryName,
                ProductName = x.ProductName,
                Price = x.Price,
            }).FirstOrDefaultAsync();
        }

        public async Task<ProductCountByPriceRangeModel> ReceiveCheapestProduct()
        {
            return await _context.Products
               .OrderBy(x => x.Price)
               .Select(x => new ProductCountByPriceRangeModel
               {
                   ProductName = x.ProductName,
                   CategoryName = x.Category.CategoryName,
                   Price = x.Price
               })
               .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetAverageHamburgerPrice()
        {
            return await _context.Products
                .Where(x => x.CategoryId == (_context.Categories.Where(x => x.CategoryName == "Burger").Select(x => x.CategoryId).FirstOrDefault()))
                .AverageAsync(x=>x.Price);
        }

        public async Task<ResultOrderModel> GetOrderCountByStatus()
        {
            return new ResultOrderModel
            {
                OrderCount = _context.Orders.Count(),
                ActiveOrderCount= _context.Orders.Count(x=>x.Description=="Müşteri Masada"),
                PassiveOrderCount= _context.Orders.Count(x=>x.Description=="Hesap Kapatıldı")
            };
        }

        public async Task<Order> GetLastOrderAsync()
        {
            return await _context.Orders.OrderByDescending(x => x.OrderId).FirstOrDefaultAsync();
        }
    }
}
