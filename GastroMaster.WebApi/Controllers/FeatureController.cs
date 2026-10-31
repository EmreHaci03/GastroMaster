using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.FeatureDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService featureService;
        private readonly IMapper mapper;
        private readonly IValidator<CreateFeatureDto> _validator;
        private readonly IValidator<UpdateFeatureDto> _updateValidator;

        public FeatureController(IFeatureService featureService, IMapper mapper, IValidator<CreateFeatureDto> validator, IValidator<UpdateFeatureDto> updateValidator)
        {
            this.featureService = featureService;
            this.mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureList()
        {
            var values = await featureService.TGetAllAsync();
            var result = mapper.Map<List<ResultFeatureDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(int id)
        {
            var value = await featureService.TGetByIdAsync(id);
            var result = mapper.Map<GetFeatureByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var validationResult = _validator.Validate(createFeatureDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new {x.PropertyName,x.ErrorMessage}));

            var value = mapper.Map<Feature>(createFeatureDto);
            await featureService.TInsertAsync(value);
            return Ok("Öne çıkan alan eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var validationResult = _updateValidator.Validate(updateFeatureDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            

            var value = mapper.Map<Feature>(updateFeatureDto);
            await featureService.TUpdateAsync(value);
            return Ok("Öne çıkan alan güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var value = await featureService.TGetByIdAsync(id);
            await featureService.TDeleteAsync(value);
            return Ok("Öne çıkan alan silindi");
        }
    }
}