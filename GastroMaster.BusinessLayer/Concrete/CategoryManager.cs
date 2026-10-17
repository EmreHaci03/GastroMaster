using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task TDeleteAsync(Category t)
        {
            await _categoryDal.DeleteAsync(t);
        }

        public async Task<List<Category>> TGetAllAsync()
        {
            return await _categoryDal.GetAllAsync();
        }

        public async Task<Category?> TGetByIdAsync(int id)
        {
            return await _categoryDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Category t)
        {
            await _categoryDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Category t)
        {
            await _categoryDal.UpdateAsync(t);
        }
    }
}
