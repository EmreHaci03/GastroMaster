using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public async Task TDeleteAsync(Contact t)
        {
            await _contactDal.DeleteAsync(t);
        }

        public async Task<List<Contact>> TGetAllAsync()
        {
            return await _contactDal.GetAllAsync();
        }

        public async Task<Contact?> TGetByIdAsync(int id)
        {
            return await _contactDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Contact t)
        {
            await _contactDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Contact t)
        {
            await _contactDal.UpdateAsync(t);
        }
    }
}