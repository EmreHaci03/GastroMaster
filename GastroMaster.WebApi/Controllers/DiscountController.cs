using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.DiscountDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService discountService;
        private readonly IMapper mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            this.discountService = discountService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountList()
        {
            var values = await discountService.TGetAllAsync();
            var result = mapper.Map<List<ResultDiscountDto>>(values);
            return Ok(result);
        }
        [HttpGet("GetTrueDiscountStatus")]
        public async Task<IActionResult> GetTrueDiscountStatus()
        {
            var values = await discountService.TGetStatusTrueDiscountList();
            var result = mapper.Map<List<ResultDiscountDto>>(values);
            return Ok(result);
        }

        [HttpPut("ChangeStatusToTrue/{id}")]
        public async Task<IActionResult> ChangeStatusToTrue(int id)
        {
            await discountService.TChangeStatusToTrue(id);
            return Ok("İndirim aktif hale getirildi");
        }

        [HttpPut("ChangeStatusToFalse/{id}")]
        public async Task<IActionResult> ChangeStatusToFalse(int id)
        {
            await discountService.TChangeStatusToFalse(id);
            return Ok("İndirim Pasif Hale Getirildi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountById(int id)
        {
            var value = await discountService.TGetByIdAsync(id);
            var result = mapper.Map<GetDiscountByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            var value = mapper.Map<Discount>(createDiscountDto);
            await discountService.TInsertAsync(value);
            return Ok("İndirim eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            var value = mapper.Map<Discount>(updateDiscountDto);
            await discountService.TUpdateAsync(value);
            return Ok("İndirim güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var value = await discountService.TGetByIdAsync(id);
            await discountService.TDeleteAsync(value);
            return Ok("İndirim silindi");
        }
    }
}