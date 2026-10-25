using GastroMaster.DataAccessLayer.Models;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IProductDal : IGenericDal<Product>
    {
        Task<List<Product>> GetProductListWithCategoryAsync();
        Task<Product?> GetProductWithCategoryByIdAsync(int id);
        Task<MaxPriceHamburgerModel> GetMostExpensiveBurgerAsync();
    }
}
