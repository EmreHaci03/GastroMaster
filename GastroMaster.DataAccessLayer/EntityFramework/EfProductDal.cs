using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        private readonly GastroMasterContext _context;
        public EfProductDal(GastroMasterContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<MaxPriceHamburgerModel> GetMostExpensiveBurgerAsync()
        {
            return await _context.Products.Where(x => x.Category.CategoryName=="Burger").Select(x => new MaxPriceHamburgerModel
            {
                ProductName=x.ProductName,
                Price=x.Price,
                Description=x.Description,
                ImageUrl=x.ImageUrl
            }).OrderByDescending(x=>x.Price).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductListWithCategoryAsync()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product?> GetProductWithCategoryByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x=>x.ProductId==id);
        }
    }
}
