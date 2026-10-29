using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.BasketDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BasketList()
        {
            var values = await _basketService.TGetAllAsync();
            var result = _mapper.Map<List<ResultBasketDto>>(values);
            return Ok(result);
        }

        [HttpGet("BasketListWithDetail")]
        public async Task<IActionResult> BasketListWithDetail()
        {
            var values = await _basketService.TGetBasketListWithDetail();
            var result = _mapper.Map<List<ResultBasketWithDetailsDto>>(values);
            return Ok(result);
        }

        [HttpGet("GetBasketItem/{menuTableId}/{productId}")]
        public async Task<IActionResult> GetBasketItem(int menuTableId, int productId)
        {
            var values = await _basketService.TGetBasketItemAsync(menuTableId, productId);
            var result = _mapper.Map<GetBasketWithDetailsByIdDto>(values);
            return Ok(result);
        }

        [HttpGet("GetBasketWithDetailById/{id}")]
        public async Task<IActionResult> GetBasketWithDetailById(int id)
        {
            var value = await _basketService.TGetBasketWithDetailById(id);
            var result = _mapper.Map<GetBasketWithDetailsByIdDto>(value);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasketById(int id)
        {
            var value = await _basketService.TGetByIdAsync(id);
            var result = _mapper.Map<GetBasketByIdDto>(value);
            return Ok(result);
        }

        [HttpGet("GetBasketByMenuTableId/{menuTableId}")]
        public async Task<IActionResult> GetBasketByMenuTableId(int menuTableId)
        {
            var values = await _basketService.TGetBasketListByTableNumber(menuTableId);
            var result = _mapper.Map<List<ResultBasketWithDetailsDto>>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateBasketDto createBasketDto)
        {
            var value = _mapper.Map<Basket>(createBasketDto);
            value.TotalPrice = value.Count * value.Price;
            await _basketService.TInsertAsync(value);
            return Ok("Sepet kaydı eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasket(UpdateBasketDto updateBasketDto)
        {
            var value = _mapper.Map<Basket>(updateBasketDto);
            value.TotalPrice = value.Count * value.Price;
            await _basketService.TUpdateAsync(value);
            return Ok("Sepet kaydı güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            var value = await _basketService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Sepet kaydı bulunamadı");
            }
            await _basketService.TDeleteAsync(value);
            return Ok("Ürün sepetten çıkarıldı");
        }
    }
}