using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.AboutItemDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutItemController : ControllerBase
    {
        private readonly IAboutItemService _aboutItemService;
        private readonly IMapper _mapper;

        public AboutItemController(IAboutItemService aboutItemService, IMapper mapper)
        {
            _aboutItemService = aboutItemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AboutItemList()
        {
            var values = await _aboutItemService.TGetAllAsync();
            var result = _mapper.Map<List<ResultAboutItemDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutItemById(int id)
        {
            var value = await _aboutItemService.TGetByIdAsync(id);
            var result = _mapper.Map<GetAboutItemByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutItem(CreateAboutItemDto createAboutItemDto)
        {
            var value = _mapper.Map<AboutItem>(createAboutItemDto);
            await _aboutItemService.TInsertAsync(value);
            return Ok("Hakkımızda maddesi eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAboutItem(UpdateAboutItemDto updateAboutItemDto)
        {
            var value = _mapper.Map<AboutItem>(updateAboutItemDto);
            await _aboutItemService.TUpdateAsync(value);
            return Ok("Hakkımızda maddesi güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutItem(int id)
        {
            var value = await _aboutItemService.TGetByIdAsync(id);
            await _aboutItemService.TDeleteAsync(value);
            return Ok("Hakkımızda maddesi silindi");
        }
    }
}