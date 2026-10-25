using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfNewsletterDal : GenericRepository<Newsletter>, INewsletterDal
    {
        private readonly GastroMasterContext _context;
        public EfNewsletterDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> MailExistsAsync(string email)
        {
            return await _context.Newsletters.AnyAsync(x=>x.Email== email);
        }
    }
}
