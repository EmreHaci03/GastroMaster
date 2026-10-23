using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DtoLayer.DTOS.OrderDtos;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task TDeleteAsync(Order t)
        {
            await _orderDal.DeleteAsync(t);
        }

        public async Task<List<Order>> TGetAllAsync()
        {
            return await _orderDal.GetAllAsync();
        }

        public async Task<Order?> TGetByIdAsync(int id)
        {
            return await _orderDal.GetByIdAsync(id);
        }


        public async Task TInsertAsync(Order t)
        {
            await _orderDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Order t)
        {
            await _orderDal.UpdateAsync(t);
        }
    }
}