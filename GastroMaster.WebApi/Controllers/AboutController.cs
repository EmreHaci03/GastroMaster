using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.AboutDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService aboutService;
        private readonly IMapper mapper;

        public AboutController(IAboutService aboutService, IMapper mapper)
        {
            this.aboutService = aboutService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await aboutService.TGetAllAsync();
            var result = mapper.Map<List<ResultAboutDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(int id)
        {
            var value = await aboutService.TGetByIdAsync(id);
            var result = mapper.Map<GetAboutByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var value = mapper.Map<About>(createAboutDto);
            await aboutService.TInsertAsync(value);
            return Ok("Hakkımızda alanı eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var value = mapper.Map<About>(updateAboutDto);
            await aboutService.TUpdateAsync(value);
            return Ok("Hakkımızda alanı güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            var value = await aboutService.TGetByIdAsync(id);
            await aboutService.TDeleteAsync(value);
            return Ok("Hakkımızda alanı silindi");
        }
    }
}