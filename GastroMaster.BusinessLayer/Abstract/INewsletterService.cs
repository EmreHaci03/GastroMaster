using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Abstract
{
    public interface INewsletterService:IGenericService<Newsletter>
    {
        Task<bool> TMailExistsAsync(string email);
    }
}
