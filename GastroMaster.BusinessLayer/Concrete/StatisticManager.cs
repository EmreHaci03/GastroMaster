using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DtoLayer.DTOS.OrderDtos;
using GastroMaster.DtoLayer.DTOS.StatisticDtos;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class StatisticManager : IStatisticService
    {
        private readonly IStatisticDal _statisticDal;
        private readonly IMapper _mapper;

        public StatisticManager(IStatisticDal statisticDal, IMapper mapper)
        {
            _statisticDal = statisticDal;
            _mapper = mapper;
        }

        public async Task<decimal> TGetAverageHamburgerPrice()
        {
            return await _statisticDal.GetAverageHamburgerPrice();
        }

        public async Task<ProductCountByPriceRangeDto> TGetCheapestProductAsync()
        {
            var value = await _statisticDal.ReceiveCheapestProduct();
            return _mapper.Map<ProductCountByPriceRangeDto>(value);
        }

        public async Task<ResultOrderDto> TGetLastOrderAsync()
        {
            var values= await _statisticDal.GetLastOrderAsync();
            return _mapper.Map<ResultOrderDto>(values);
            
        }

        public async Task<ResultOrderCountDto> TGetOrderCountByStatus()
        {
            var value= await _statisticDal.GetOrderCountByStatus();
            return _mapper.Map<ResultOrderCountDto>(value); 
        }

        public async  Task<GetStatisticDto> TGetStatisticModelsAsync()
        {
            var value = await _statisticDal.GetStatisticModelsAsync();
            return _mapper.Map<GetStatisticDto>(value);
        }

        public async Task<ProductCountByCategoryDto> TProductCountByCategoryAsync()
        {
            var value = await _statisticDal.ProductCountByCategoryAsync();
            return _mapper.Map<ProductCountByCategoryDto>(value);
        }

        public async Task<ProductCountByPriceRangeDto> TReceiveMostExpensiveProduct()
        {
            var value = await _statisticDal.ReceiveMostExpensiveProduct();
            return _mapper.Map<ProductCountByPriceRangeDto>(value);
        }
    }
}