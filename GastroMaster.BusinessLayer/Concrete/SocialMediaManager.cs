using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class SocialMediaManager:ISocialMediaService
    {
        private readonly ISocialMediaDal _socialMediaDal;

        public SocialMediaManager(ISocialMediaDal socialMediaDal)
        {
            _socialMediaDal = socialMediaDal;
        }

        public async Task TDeleteAsync(SocialMedia t)
        {
            await _socialMediaDal.DeleteAsync(t);
        }

        public async Task<List<SocialMedia>> TGetAllAsync()
        {
            return await _socialMediaDal.GetAllAsync();
        }

        public async Task<SocialMedia?> TGetByIdAsync(int id)
        {
            return await _socialMediaDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(SocialMedia t)
        {
            await _socialMediaDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(SocialMedia t)
        {
            await _socialMediaDal.UpdateAsync(t);
        }
    }
}
