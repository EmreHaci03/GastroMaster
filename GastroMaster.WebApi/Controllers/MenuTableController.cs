using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.MenuTableDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuTableController : ControllerBase
    {
        private readonly IMenuTableService _menuTableService;
        private readonly IMapper _mapper;

        public MenuTableController(IMenuTableService menuTableService, IMapper mapper)
        {
            _menuTableService = menuTableService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> MenuTableList()
        {
            var values = await _menuTableService.TGetAllAsync();
            var result = _mapper.Map<List<ResultMenuTableDto>>(values);
            return Ok(result);
        }

        [HttpGet("GetMenuWithOrderStatus")]
        public async Task<IActionResult> GetMenuWithOrderStatus()
        {
            var values=await _menuTableService.TGetMenuTableWithOrderStatus();
            var result=_mapper.Map<List<GetMenuTableWithOrderStatusDto>>(values);
            return Ok(result);
        }

        [HttpGet("GetMenuTableCount")]
        public async Task<IActionResult> MenuTableCount()
        {
            var values = await _menuTableService.TMenuTableCount();
            return Ok(values);
        }

        [HttpGet("GetActiveMenuTableCount")]
        public async Task<IActionResult> ActiveMenuTableCount()
        {
            var values = await _menuTableService.TActiveMenuTableCount();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuTableById(int id)
        {
            var value = await _menuTableService.TGetByIdAsync(id);
            var result = _mapper.Map<GetMenuTableByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuTable(CreateMenuTableDto createMenuTableDto)
        {
            var value = _mapper.Map<MenuTable>(createMenuTableDto);
            await _menuTableService.TInsertAsync(value);
            return Ok("Masa eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuTable(UpdateMenuTableDto updateMenuTableDto)
        {
            var value = _mapper.Map<MenuTable>(updateMenuTableDto);
            await _menuTableService.TUpdateAsync(value);
            return Ok("Masa güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuTable(int id)
        {
            var value = await _menuTableService.TGetByIdAsync(id);
            await _menuTableService.TDeleteAsync(value);
            return Ok("Masa silindi");
        }
    }
}