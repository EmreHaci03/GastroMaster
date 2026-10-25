using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfAboutItemDal : GenericRepository<AboutItem>, IAboutItemDal
    {
        public EfAboutItemDal(GastroMasterContext context) : base(context)
        {
        }
    }
}
