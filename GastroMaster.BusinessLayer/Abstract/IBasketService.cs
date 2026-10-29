using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IBasketService:IGenericService<Basket>
    {
        Task<List<Basket>> TGetBasketListWithDetail();
        Task<Basket?> TGetBasketWithDetailById(int id);
        Task<List<Basket>> TGetBasketListByTableNumber(int id);
        Task<Basket?> TGetBasketItemAsync(int menuTableId, int productId);
    }
}

