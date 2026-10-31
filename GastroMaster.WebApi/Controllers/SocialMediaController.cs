using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.SocialMediaDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService socialMediaService;
        private readonly IMapper mapper;
        private readonly IValidator<CreateSocialMediaDto> _validator;
        private readonly IValidator<UpdateSocialMediaDto> _updateValidator;

        public SocialMediaController(ISocialMediaService socialMediaService, IMapper mapper, IValidator<CreateSocialMediaDto> validator, IValidator<UpdateSocialMediaDto> updateValidator)
        {
            this.socialMediaService = socialMediaService;
            this.mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> SocialMediaList()
        {
            var values = await socialMediaService.TGetAllAsync();
            var result = mapper.Map<List<ResultSocialMediaDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialMediaById(int id)
        {
            var value = await socialMediaService.TGetByIdAsync(id);
            var result = mapper.Map<GetSocialMediaByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var validationResult = _validator.Validate(createSocialMediaDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            var value = mapper.Map<SocialMedia>(createSocialMediaDto);
            await socialMediaService.TInsertAsync(value);
            return Ok("Sosyal medya eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var validationResult = _updateValidator.Validate(updateSocialMediaDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new {x.PropertyName,x.ErrorMessage}));
            var value = mapper.Map<SocialMedia>(updateSocialMediaDto);
            await socialMediaService.TUpdateAsync(value);
            return Ok("Sosyal medya güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var value = await socialMediaService.TGetByIdAsync(id);
            await socialMediaService.TDeleteAsync(value);
            return Ok("Sosyal medya silindi");
        }
    }
}