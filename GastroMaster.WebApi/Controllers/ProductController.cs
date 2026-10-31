using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.ContactDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductDto> _validator;

        public ProductController(IProductService productService, IMapper mapper, IValidator<CreateProductDto> validator)
        {
            _productService = productService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.TGetAllAsync();
            var result = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(result);
        }

        [HttpGet("GetMostExpensiveBurger")]
        public async Task<IActionResult> MostExpensiveBurger()
        {
            var values = await _productService.TGetMostExpensiveBurgerAsync();
            var result = _mapper.Map<MaxPriceHamburgerDto>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var values = await _productService.TGetByIdAsync(id);
            var result = _mapper.Map<GetProductByIdDto>(values);
            return Ok(result);
        }

        [HttpGet("GetProductListWithCategory")]
        public async Task<IActionResult> GetProductListWithCategory()
        {
            var values = await _productService.TGetProductListWithCategoryAsync();
            var result = _mapper.Map<List<ResultProductWithCategoryDto>>(values);
            return Ok(result);
        }

        [HttpGet("GetProductListWithCategoryById")]
        public async Task<IActionResult> GetProductListWithCategoryById(int id)
        {
            var values = await _productService.TGetProductWithCategoryByIdAsync(id);
            var result = _mapper.Map<GetProductWithCategoryByIdDto>(values);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var value = _mapper.Map<Product>(dto);
            await _productService.TInsertAsync(value);
            return Ok("Ürün Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            var value = _mapper.Map<Product>(dto);
            await _productService.TUpdateAsync(value);
            return Ok("Ürün Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var value = await _productService.TGetByIdAsync(id);
            await _productService.TDeleteAsync(value);
            return Ok("Ürün Silindi");
        }


    }
}
