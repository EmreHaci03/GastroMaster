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
    public class TimelineManager:ITimelineService
    {
         private readonly ITimelineDal _timelineDal;

        public TimelineManager(ITimelineDal timelineDal)
        {
            _timelineDal = timelineDal;
        }

        public async Task TDeleteAsync(Timeline t)
        {
            await _timelineDal.DeleteAsync(t);
        }

        public async Task<List<Timeline>> TGetAllAsync()
        {
            return await _timelineDal.GetAllAsync();
        }

        public async Task<Timeline?> TGetByIdAsync(int id)
        {
            return await _timelineDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Timeline t)
        {
            await _timelineDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Timeline t)
        {
            await _timelineDal.UpdateAsync(t);
        }
    }
}
