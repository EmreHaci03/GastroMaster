using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class ProductManager:IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task TDeleteAsync(Product t)
        {
            await _productDal.DeleteAsync(t);
        }

        public async Task<List<Product>> TGetAllAsync()
        {
            return await _productDal.GetAllAsync();
        }

        public async Task<Product?> TGetByIdAsync(int id)
        {
            return await _productDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Product t)
        {
            await _productDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Product t)
        {
            await _productDal.UpdateAsync(t);
        }
    }
}
