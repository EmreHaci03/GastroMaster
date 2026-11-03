using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DtoLayer.DTOS.OrderDtos;
using GastroMaster.DtoLayer.DTOS.StatisticDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
       private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;
        public StatisticController(IStatisticService statisticService, IMapper mapper)
        {
            _statisticService = statisticService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> TGetStatisticModelsAsync()
        {
            var values=await _statisticService.TGetStatisticModelsAsync();
            var result=_mapper.Map<GetStatisticDto>(values);
            return Ok(result);
        }

        [HttpGet("GetLastOrder")]
        public async Task<IActionResult> GetLastOrder()
        {
            var values = await _statisticService.TGetLastOrderAsync();
            var result=_mapper.Map<ResultOrderDto>(values);
            return Ok(result);
        }


        [HttpGet("OrderStatistic")]
        public async Task<IActionResult> OrderStatistic()
        {
            var values = await _statisticService.TGetOrderCountByStatus();
            var result = _mapper.Map<ResultOrderCountDto>(values);
            return Ok(result);
        }

        [HttpGet("HamburgerAveragePrice")]
        public async Task<IActionResult> HamburgerAveragePrice()
        {
            var result = await _statisticService.TGetAverageHamburgerPrice();
            return Ok(result);
        }

        [HttpGet("MostProductByCategory")]
        public async Task<IActionResult> MostProductByCategoryAsync()
        {
            var values = await _statisticService.TProductCountByCategoryAsync();
            var result = _mapper.Map<ProductCountByCategoryDto>(values);
            return Ok(result);
        }


        [HttpGet("CheapestProduct")]
        public async Task<IActionResult> CheapestProduct()
        {
            var values = await _statisticService.TGetCheapestProductAsync();
            var result = _mapper.Map<ProductCountByPriceRangeDto>(values);
            return Ok(result);
        }

        [HttpGet("MostExpensiveProduct")]
        public async Task<IActionResult> MostExpensiveProduct()
        {
            var values = await _statisticService.TReceiveMostExpensiveProduct();
            var result = _mapper.Map<ProductCountByPriceRangeDto>(values);
            return Ok(result);
        }
    }
}