using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.OrderDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> OrderList()
        {
            var values = await orderService.TGetAllAsync();
            var result = mapper.Map<List<ResultOrderDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var value = await orderService.TGetByIdAsync(id);
            var result = mapper.Map<GetOrderByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            var value = mapper.Map<Order>(createOrderDto);
            await orderService.TInsertAsync(value);
            return Ok("Sipariş eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            var value = mapper.Map<Order>(updateOrderDto);
            await orderService.TUpdateAsync(value);
            return Ok("Sipariş güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var value = await orderService.TGetByIdAsync(id);
            await orderService.TDeleteAsync(value);
            return Ok("Sipariş silindi");
        }
    }
}