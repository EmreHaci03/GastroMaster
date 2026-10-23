using GastroMaster.BusinessLayer.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace GastroMaster.WebApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IStatisticService _statisticService;

        public SignalRHub(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task SendStatistics()
        {
            var statistic = await _statisticService.TGetStatisticModelsAsync();
            await Clients.All.SendAsync("ReceiveStatistics", statistic);

            var orderStatistic = await _statisticService.TGetOrderCountByStatus(); 
            await Clients.All.SendAsync("ReceiveOrderStatistic", orderStatistic);

            var lastOrder = await _statisticService.TGetLastOrderAsync();
            await Clients.All.SendAsync("ReceiveLastOrder", lastOrder); 

            var hamburgerAveragePrice = await _statisticService.TGetAverageHamburgerPrice();
            await Clients.All.SendAsync("ReceiveHamburgerAveragePrice", hamburgerAveragePrice);

            var mostProductByCategory = await _statisticService.TProductCountByCategoryAsync();
            await Clients.All.SendAsync("ReceiveMostProductByCategory", mostProductByCategory);

            var cheapestProduct = await _statisticService.TGetCheapestProductAsync();
            await Clients.All.SendAsync("ReceiveCheapestProduct", cheapestProduct);

            var mostExpensiveProduct = await _statisticService.TReceiveMostExpensiveProduct();
            await Clients.All.SendAsync("ReceiveMostExpensiveProduct", mostExpensiveProduct);
        }
    }
}