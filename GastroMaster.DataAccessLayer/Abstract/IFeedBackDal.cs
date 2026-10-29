using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IFeedBackDal:IGenericDal<FeedBack>
    {
        Task MarkAsReadAsync(int id);
        Task MarkAsUnreadAsync(int id);
    }
}
