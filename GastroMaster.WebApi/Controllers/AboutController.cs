using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<CreateAboutDto> _validator;
        private readonly IValidator<UpdateAboutDto> _UpdateValidator;

        public AboutController(IAboutService aboutService, IMapper mapper, IValidator<CreateAboutDto> validator, IValidator<UpdateAboutDto> updateValidator)
        {
            this.aboutService = aboutService;
            this.mapper = mapper;
            _validator = validator;
            _UpdateValidator = updateValidator;
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
            var validationResult = _validator.Validate(createAboutDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => new {x.PropertyName,x.ErrorMessage}));
            }
            var value = mapper.Map<About>(createAboutDto);
            await aboutService.TInsertAsync(value);
            return Ok("Hakkımızda alanı eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var validationResult = _UpdateValidator.Validate(updateAboutDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

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