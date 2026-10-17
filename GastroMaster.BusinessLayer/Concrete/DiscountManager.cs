using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class DiscountManager:IDiscountService
    {
        private readonly IDiscountDal _discountDal;

        public DiscountManager(IDiscountDal discountDal)
        {
            _discountDal = discountDal;
        }

        public async Task TDeleteAsync(Discount t)
        {
            await _discountDal.DeleteAsync(t);
        }

        public async Task<List<Discount>> TGetAllAsync()
        {
            return await _discountDal.GetAllAsync();
        }

        public async Task<Discount?> TGetByIdAsync(int id)
        {
            return await _discountDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Discount t)
        {
            await _discountDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Discount t)
        {
            await _discountDal.UpdateAsync(t);
        }
    }
}
