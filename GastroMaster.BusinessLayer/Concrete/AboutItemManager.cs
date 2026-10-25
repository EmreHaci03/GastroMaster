using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class AboutItemManager:IAboutItemService
    {
        private readonly IAboutItemDal _aboutItemDal;

        public AboutItemManager(IAboutItemDal aboutItemDal)
        {
            _aboutItemDal = aboutItemDal;
        }

        public async Task TDeleteAsync(AboutItem t)
        {
            await _aboutItemDal.DeleteAsync(t);
        }

        public async Task<List<AboutItem>> TGetAllAsync()
        {
            return await _aboutItemDal.GetAllAsync();
        }

        public async Task<AboutItem?> TGetByIdAsync(int id)
        {
           return await _aboutItemDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(AboutItem t)
        {
            await _aboutItemDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(AboutItem t)
        {
            await _aboutItemDal.UpdateAsync(t);
        }
    }
}
