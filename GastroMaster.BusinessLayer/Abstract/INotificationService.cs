using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Abstract
{
    public interface INotificationService:IGenericService<Notification>
    {
        Task<List<Notification>> TGetNotificationListByFalseStatus();
        Task TChangeNotificationStatusByFalse(int id);
        Task TChangeNotificationStatusByTrue(int id);
        Task<int> TNotificationCountByStatusFalse();
    }
}
