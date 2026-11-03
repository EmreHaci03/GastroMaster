using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.NewsletterDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]

    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;
        private readonly IMapper _mapper;

        public NewsletterController(INewsletterService newsletterService, IMapper mapper)
        {
            _newsletterService = newsletterService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> NewsletterList()
        {
            var values = await _newsletterService.TGetAllAsync();
            var result = _mapper.Map<List<ResultNewsletterDto>>(values);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewsletter(CreateNewsletterDto createNewsletterDto)
        {
            var mailExists = await _newsletterService.TMailExistsAsync(createNewsletterDto.Email);
            if (mailExists)
            {
                return BadRequest("Bu mail adresi zaten kayıtlı");
            }

            var value = _mapper.Map<Newsletter>(createNewsletterDto);
            await _newsletterService.TInsertAsync(value);
            return Ok("Abonelik oluşturuldu");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsletter(int id)
        {
            var value = await _newsletterService.TGetByIdAsync(id);
            await _newsletterService.TDeleteAsync(value);
            return Ok("Abonelik silindi");
        }
    }
}