using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.OrderDetailDtos;
using GastroMaster.DtoLayer.DTOS.OrderDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService orderDetailService;
        private readonly IMapper mapper;

        public OrderDetailController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            this.orderDetailService = orderDetailService;
            this.mapper = mapper;
        }

        [HttpGet] // 
        public async Task<IActionResult> OrderDetailList()
        {
            var values = await orderDetailService.TGetAllAsync();
            var result = mapper.Map<List<ResultOrderDetailDto>>(values);
            return Ok(result);
        }


        [HttpGet("GetOrderDetailWithDetail")] //
        public async Task<IActionResult> GetOrderDetailWithDetail()
        {
            var values = await orderDetailService.TOrderDetailWithDetail();
            var result = mapper.Map<List<GetOrderDetailWithDetailDto>>(values);
            return Ok(result);
        }

        [HttpGet("GetOrderDetailWithDetail/{id}")] //
        public async Task<IActionResult> GetOrderDetailWithDetailById(int id)
        {
            var values = await orderDetailService.TOrderDetailWithDetailById(id);
            var result = mapper.Map<GetOrderDetailWithDetailDto>(values);
            return Ok(result);
        }
        [HttpGet("GetOrderDetailByProduct/{productId}")]
        public async Task<IActionResult> GetOrderDetailByProduct(int productId)
        {
            var values = await orderDetailService.TOrderDetailWithDetailByProductId(productId);
            var result = mapper.Map<GetOrderDetailWithDetailDto>(values);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var value = await orderDetailService.TGetByIdAsync(id);
            var result = mapper.Map<GetOrderDetailByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            var value = mapper.Map<OrderDetail>(createOrderDetailDto);
            await orderDetailService.TInsertAsync(value);
            return Ok("Sipariş detayı eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailDto updateOrderDetailDto)
        {
            var value = mapper.Map<OrderDetail>(updateOrderDetailDto);
            await orderDetailService.TUpdateAsync(value);
            return Ok("Sipariş detayı güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var value = await orderDetailService.TGetByIdAsync(id);
            await orderDetailService.TDeleteAsync(value);
            return Ok("Sipariş detayı silindi");
        }
    }
}