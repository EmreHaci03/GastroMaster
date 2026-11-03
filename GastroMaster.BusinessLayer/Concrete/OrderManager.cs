using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Models;
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

        public async Task<List<Order>> TGetOrderWithDetail()
        {
            return await _orderDal.GetOrderWithDetail();
        }


        public async Task<decimal> TGetTodayRevenueAsync()
        {
            return await _orderDal.GetTodayRevenueAsync();
        }

        public async Task TInsertAsync(Order t)
        {
            await _orderDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Order t)
        {
            await _orderDal.UpdateAsync(t);
        }

        public async Task<Order> TGetOrderWithDetailById(int id)
        {
            return await _orderDal.GetOrderWithDetailById(id);
        }

        public async Task<int> TGetClosedOrderCountAsync()
        {
            return await _orderDal.GetClosedOrderCountAsync();
        }

        public async Task<int> TGetOccupiedMenuTableCountAsync()
        {
            return await _orderDal.GetOccupiedMenuTableCountAsync();
        }

        public async Task<List<DailyRevenueModel>> TGetLastWeekDailyRevenueAsync()
        {
            return await _orderDal.GetLastWeekDailyRevenueAsync();
        }

        public async Task<Order> TGetOrderByMenuTable(int id)
        {
            return await _orderDal.GetOrderByMenuTable(id);
        }
    }
}