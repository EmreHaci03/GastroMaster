using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DtoLayer.DTOS.OrderDetailDtos;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public async Task<List<OrderDetail>> TOrderDetailWithDetail()
        {
            return await _orderDetailDal.OrderDetailWithDetail();
        }

        public async Task<OrderDetail> TOrderDetailWithDetailById(int id)
        {
            return await _orderDetailDal.OrderDetailWithDetailById(id);
        }

        public async Task<OrderDetail> TOrderDetailWithDetailByProductId(int id)
        {
            return await _orderDetailDal.OrderDetailWithDetailByProductId(id);
        }

        public async Task TDeleteAsync(OrderDetail t)
        {
            await _orderDetailDal.DeleteAsync(t);
        }

        public async Task<List<OrderDetail>> TGetAllAsync()
        {
            return await _orderDetailDal.GetAllAsync();
        }

        public async Task<OrderDetail?> TGetByIdAsync(int id)
        {
            return await _orderDetailDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(OrderDetail t)
        {
            await _orderDetailDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(OrderDetail t)
        {
            await _orderDetailDal.UpdateAsync(t);
        }

        public async Task<List<TopSellingProductModels>> TGetTopSellingProductsAsync()
        {
            return await _orderDetailDal.GetTopSellingProductsAsync();
        }

        public async Task<List<GetLastOrderModel>> TGetLastOrders()
        {
            return await _orderDetailDal.GetLastOrders();
        }
    }
}