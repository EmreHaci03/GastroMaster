using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfFeedBackDal : GenericRepository<FeedBack>, IFeedBackDal
    {
        private readonly GastroMasterContext _context;
        public EfFeedBackDal(GastroMasterContext context) : base(context)
        {
            _context=context;
        }

        public async Task MarkAsReadAsync(int id)
        {
            var FeedBack = await _context.FeedBacks.FirstOrDefaultAsync(x => x.FeedbackId == id);
            if (FeedBack != null)
            {
                FeedBack.IsRead= true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsUnreadAsync(int id)
        {
            var FeedBack = await _context.FeedBacks.FirstOrDefaultAsync(x => x.FeedbackId == id);
            if (FeedBack != null)
            {
                FeedBack.IsRead = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
