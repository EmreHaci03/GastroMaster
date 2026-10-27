using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfNotificationDal : GenericRepository<Notification>, INotificationDal
    {
        private readonly GastroMasterContext _context;
        public EfNotificationDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task ChangeNotificationStatusByFalse(int id)
        {
            var value = await _context.Notifications.FindAsync(id);
              if (value != null)
            {
                value.Status = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeNotificationStatusByTrue(int id)
        {
           var value=await _context.Notifications.FindAsync(id);
            if (value != null)
            {
                value.Status= true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Notification>> GetNotificationListByFalseStatus()
        {
            return await _context.Notifications.Where(x => !x.Status).ToListAsync();
        }

        public async Task<int> NotificationCountByStatusFalse()
        {
            return await _context.Notifications.CountAsync(x => !x.Status);
        }
    }
}
