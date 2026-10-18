using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class AboutManager:IAboutService
    {
        private readonly IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public async Task TDeleteAsync(About t)
        {
            await _aboutDal.DeleteAsync(t);
        }

        public async Task<List<About>> TGetAllAsync()
        {
            return await _aboutDal.GetAllAsync();
        }

        public async Task<About?> TGetByIdAsync(int id)
        {
            return await _aboutDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(About t)
        {
            await _aboutDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(About t)
        {
            await _aboutDal.UpdateAsync(t);
        }
    }
}
