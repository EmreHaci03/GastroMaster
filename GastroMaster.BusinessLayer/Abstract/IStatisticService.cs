using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DtoLayer.DTOS.OrderDtos;
using GastroMaster.DtoLayer.DTOS.StatisticDtos;

namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IStatisticService
    {
        Task<GetStatisticDto> TGetStatisticModelsAsync();
        Task<ProductCountByCategoryDto> TProductCountByCategoryAsync();
        Task<ProductCountByPriceRangeDto> TGetCheapestProductAsync();
        Task<ProductCountByPriceRangeDto> TReceiveMostExpensiveProduct();
        Task<decimal> TGetAverageHamburgerPrice();
        Task<ResultOrderCountDto> TGetOrderCountByStatus();
        Task<ResultOrderDto> TGetLastOrderAsync();
    }
}