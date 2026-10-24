using GastroMaster.BusinessLayer.Abstract;
using Microsoft.AspNetCore.SignalR;

namespace GastroMaster.WebApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IStatisticService _statisticService;
        private readonly ICashRegisterService _cashRegisterService;
        private readonly IOrderService _orderService;
        private readonly IMenuTableService _menuTableService;
        public SignalRHub(IStatisticService statisticService, ICashRegisterService cashRegisterService, IOrderService orderService, IMenuTableService menuTableService)
        {
            _statisticService = statisticService;
            _cashRegisterService = cashRegisterService;
            _orderService = orderService;
            _menuTableService = menuTableService;
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

            var CashRegisterTotalAmount = await _cashRegisterService.TGetTotalAmountAsync();
            await Clients.All.SendAsync("ReceiveCashRegisterTotalAmount", CashRegisterTotalAmount);

            var TodayOrderAmount = await _orderService.TGetTodayRevenueAsync();
            await Clients.All.SendAsync("ReceiveTodayOrderAmount", TodayOrderAmount);

            var MenuTableCount = await _menuTableService.TMenuTableCount();
            await Clients.All.SendAsync("ReceiveMenuTableCount", MenuTableCount);

            var ActiveMenuTableCount = await _menuTableService.TActiveMenuTableCount();
            await Clients.All.SendAsync("ReceiveActiveMenuTableCount", ActiveMenuTableCount);
        }
    }
}