using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface INotificationDal:IGenericDal<Notification>
    {
        Task<List<Notification>> GetNotificationListByFalseStatus();
        Task ChangeNotificationStatusByFalse(int id); 
        Task ChangeNotificationStatusByTrue(int id);
        Task<int> NotificationCountByStatusFalse();
    }
}
