using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfTimeLineDal : GenericRepository<Timeline>, ITimelineDal
    {
        public EfTimeLineDal(GastroMasterContext context) : base(context)
        {
        }
    }
}
