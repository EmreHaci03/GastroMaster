using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class FeedBackManager:IFeedBackService
    {
        private readonly IFeedBackDal _feedBackDal;

        public FeedBackManager(IFeedBackDal feedBackDal)
        {
            _feedBackDal = feedBackDal;
        }

        public async Task TDeleteAsync(FeedBack t)
        {
            await _feedBackDal.DeleteAsync(t);
        }

        public async Task<List<FeedBack>> TGetAllAsync()
        {
            return await _feedBackDal.GetAllAsync();
        }

        public async Task<FeedBack?> TGetByIdAsync(int id)
        {
            return await _feedBackDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(FeedBack t)
        {
            await _feedBackDal.InsertAsync(t);   
        }

        public async Task TMarkAsReadAsync(int id)
        {
            await _feedBackDal.MarkAsReadAsync(id);
        }

        public async Task TMarkAsUnreadAsync(int id)
        {
            await _feedBackDal.MarkAsUnreadAsync(id);
        }

        public async Task TUpdateAsync(FeedBack t)
        {
            await _feedBackDal.UpdateAsync(t);
        }
    }
}
