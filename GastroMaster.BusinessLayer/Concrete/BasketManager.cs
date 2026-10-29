using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        public async Task TDeleteAsync(Basket t)
        {
            await _basketDal.DeleteAsync(t);
        }

        public async Task<List<Basket>> TGetAllAsync()
        {
            return await _basketDal.GetAllAsync();
        }

        public async Task<Basket?> TGetBasketItemAsync(int menuTableId, int productId)
        {
            return await _basketDal.GetBasketItemAsync(menuTableId, productId);
        }

        public async Task<List<Basket>> TGetBasketListByTableNumber(int id)
        {
            return await _basketDal.GetBasketListByTableNumber(id);
        }

        public async Task<List<Basket>> TGetBasketListWithDetail()
        {
            return await _basketDal.GetBasketListWithDetail();
        }


        public async Task<Basket?> TGetBasketWithDetailById(int id)
        {
            return await _basketDal.GetBasketWithDetailById(id);
        }

        public async Task<Basket?> TGetByIdAsync(int id)
        {
            return await _basketDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Basket t)
        {
            await _basketDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Basket t)
        {
            await _basketDal.UpdateAsync(t);
        }
    }
}