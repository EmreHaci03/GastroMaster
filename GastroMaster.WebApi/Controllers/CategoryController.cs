using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.CategoryDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly IValidator<CreateCategoryDto> _validator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IValidator<CreateCategoryDto> validator, IValidator<UpdateCategoryDto> updateValidator)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await categoryService.TGetAllAsync();
            var result = mapper.Map<List<ResultCategoryDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var value = await categoryService.TGetByIdAsync(id);
            var result = mapper.Map<GetCategoryByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var validationResult = _validator.Validate(createCategoryDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var value = mapper.Map<Category>(createCategoryDto);
            await categoryService.TInsertAsync(value);
            return Ok("Kategori eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var validationResult = _updateValidator.Validate(updateCategoryDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var value = mapper.Map<Category>(updateCategoryDto);
            await categoryService.TUpdateAsync(value);
            return Ok("Kategori güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var value = await categoryService.TGetByIdAsync(id);
            await categoryService.TDeleteAsync(value);
            return Ok("Kategori silindi");
        }
    }
}