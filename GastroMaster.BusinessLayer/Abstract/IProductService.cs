using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IProductService:IGenericService<Product>
    {
        Task<List<Product>> TGetProductListWithCategoryAsync();
        Task<Product?> TGetProductWithCategoryByIdAsync(int id);
    }
}
