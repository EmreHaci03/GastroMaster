using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.SliderDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSliderDto> _validator;
        private readonly IValidator<UpdateSliderDto> _updateValidator;

        public SliderController(ISliderService sliderService, IMapper mapper, IValidator<CreateSliderDto> validator, IValidator<UpdateSliderDto> updateValidator)
        {
            _sliderService = sliderService;
            _mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SliderList()
        {
            var values = await _sliderService.TGetAllAsync();
            var result = _mapper.Map<List<ResultSliderDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSliderById(int id)
        {
            var value = await _sliderService.TGetByIdAsync(id);
            var result = _mapper.Map<GetSliderByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto createSliderDto)
        {
            var validationResult=_validator.Validate(createSliderDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var value = _mapper.Map<Slider>(createSliderDto);
            await _sliderService.TInsertAsync(value);
            return Ok("Slider eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto updateSliderDto)
        {
            var validationResult = _updateValidator.Validate(updateSliderDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var value = _mapper.Map<Slider>(updateSliderDto);
            await _sliderService.TUpdateAsync(value);
            return Ok("Slider güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var value = await _sliderService.TGetByIdAsync(id);
            await _sliderService.TDeleteAsync(value);
            return Ok("Slider silindi");
        }
    }
}