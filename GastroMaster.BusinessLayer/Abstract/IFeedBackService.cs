using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IFeedBackService:IGenericService<FeedBack>
    {
        Task TMarkAsReadAsync(int id);
        Task TMarkAsUnreadAsync(int id);
    }
}
