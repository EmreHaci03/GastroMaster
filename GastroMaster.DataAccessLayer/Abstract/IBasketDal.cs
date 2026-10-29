using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IBasketDal : IGenericDal<Basket>
    {
        Task<List<Basket>> GetBasketListWithDetail();
        Task<Basket?> GetBasketWithDetailById(int id);
        Task<List<Basket>> GetBasketListByTableNumber(int id);
        Task<Basket?> GetBasketItemAsync(int menuTableId, int productId);
    }
}