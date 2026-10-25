using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class NewsletterManager:INewsletterService
    {
        private readonly INewsletterDal _newsletterDal;

        public NewsletterManager(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        public async Task TDeleteAsync(Newsletter t)
        {
            await _newsletterDal.DeleteAsync(t);    
        }

        public async Task<List<Newsletter>> TGetAllAsync()
        {
            return await _newsletterDal.GetAllAsync();
        }

        public async Task<Newsletter?> TGetByIdAsync(int id)
        {
            return await _newsletterDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Newsletter t)
        {
            await _newsletterDal.InsertAsync(t);
        }

        public async Task<bool> TMailExistsAsync(string email)
        {
            return await _newsletterDal.MailExistsAsync(email);
        }

        public async Task TUpdateAsync(Newsletter t)
        {
            await _newsletterDal.UpdateAsync(t);
        }
    }
}
