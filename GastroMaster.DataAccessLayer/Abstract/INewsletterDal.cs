using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface INewsletterDal:IGenericDal<Newsletter>
    {
        Task<bool> MailExistsAsync(string email);
    }
}
