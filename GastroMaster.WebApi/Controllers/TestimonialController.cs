using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.TestimonialDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService testimonialService;
        private readonly IMapper mapper;
        private readonly IValidator<CreateTestimonialDto> _validator;
        private readonly IValidator<UpdateTestimonialDto> _updateValidator;

        public TestimonialController(ITestimonialService testimonialService, IMapper mapper, IValidator<CreateTestimonialDto> validator, IValidator<UpdateTestimonialDto> updateValidator)
        {
            this.testimonialService = testimonialService;
            this.mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> TestimonialList()
        {
            var values = await testimonialService.TGetAllAsync();
            var result = mapper.Map<List<ResultTestimonialDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestimonialById(int id)
        {
            var value = await testimonialService.TGetByIdAsync(id);
            var result = mapper.Map<GetTestimonialByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var validationResult = _validator.Validate(createTestimonialDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var value = mapper.Map<Testimonial>(createTestimonialDto);
            await testimonialService.TInsertAsync(value);
            return Ok("Referans eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            var validationResult = _updateValidator.Validate(updateTestimonialDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var value = mapper.Map<Testimonial>(updateTestimonialDto);
            await testimonialService.TUpdateAsync(value);
            return Ok("Referans güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var value = await testimonialService.TGetByIdAsync(id);
            await testimonialService.TDeleteAsync(value);
            return Ok("Referans silindi");
        }
    }
}