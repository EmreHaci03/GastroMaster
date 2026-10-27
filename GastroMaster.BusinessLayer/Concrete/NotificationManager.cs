using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class NotificationManager:INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task TChangeNotificationStatusByFalse(int id)
        {
            await _notificationDal.ChangeNotificationStatusByFalse(id);
        }

        public async Task TChangeNotificationStatusByTrue(int id)
        {
            await _notificationDal.ChangeNotificationStatusByTrue(id);
        }

        public async Task TDeleteAsync(Notification t)
        {
            await _notificationDal.DeleteAsync(t);
        }

        public async Task<List<Notification>> TGetAllAsync()
        {
            return await _notificationDal.GetAllAsync();
        }

        public async Task<Notification?> TGetByIdAsync(int id)
        {
            return await _notificationDal.GetByIdAsync(id);
        }

        public async Task<List<Notification>> TGetNotificationListByFalseStatus()
        {
            return await _notificationDal.GetNotificationListByFalseStatus();
        }

        public async Task TInsertAsync(Notification t)
        {
            await _notificationDal.InsertAsync(t);
        }

        public async Task<int> TNotificationCountByStatusFalse()
        {
            return await _notificationDal.NotificationCountByStatusFalse();
        }

        public async Task TUpdateAsync(Notification t)
        {
            await _notificationDal.UpdateAsync(t);
        }
    }
}
