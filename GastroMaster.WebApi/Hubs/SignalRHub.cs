using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.DiscountDtos;
using GastroMaster.DtoLayer.DTOS.OrderDetailDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.SignalR;

namespace GastroMaster.WebApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IStatisticService _statisticService;
        private readonly ICashRegisterService _cashRegisterService;
        private readonly IOrderService _orderService;
        private readonly IMenuTableService _menuTableService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        public SignalRHub(IStatisticService statisticService, ICashRegisterService cashRegisterService, IOrderService orderService, IMenuTableService menuTableService, IOrderDetailService orderDetailService, IMapper mapper, IProductService productService, IDiscountService discountService)
        {
            _statisticService = statisticService;
            _cashRegisterService = cashRegisterService;
            _orderService = orderService;
            _menuTableService = menuTableService;
            _orderDetailService = orderDetailService;
            _mapper = mapper;
            _productService = productService;
            _discountService = discountService;
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

        public async Task DashboardStatistic()
        {
            var todayRevenue = await _orderService.TGetTodayRevenueAsync();
            await Clients.All.SendAsync("ReceiveTodayOrderAmount",todayRevenue);

            var TotalCashRegisterAmount = await _cashRegisterService.TGetTotalAmountAsync();
            await Clients.All.SendAsync("ReceiveCashRegisterAmount", TotalCashRegisterAmount);

            var GetClosedOrderCount = await _orderService.TGetClosedOrderCountAsync();
            await Clients.All.SendAsync("ReceiveClosedOrderCount", GetClosedOrderCount);

            var ActiveTableCount = await _menuTableService.TActiveMenuTableCount();
            await Clients.All.SendAsync("ReceiveActiveTableCount", ActiveTableCount);

            var occupiedMenuTableCount = await _orderService.TGetOccupiedMenuTableCountAsync();
            await Clients.All.SendAsync("ReceiveOccupiedMenuTableCount", occupiedMenuTableCount);

            var TopSellingProducts=await _orderDetailService.TGetTopSellingProductsAsync();
            await Clients.All.SendAsync("ReceiveTopSellingProducts", TopSellingProducts);

            var LastOrders = await _orderDetailService.TGetLastOrders();
            await Clients.All.SendAsync("ReceiveLastOrders", LastOrders);

            var LastWeekRevenue = await _orderService.TGetLastWeekDailyRevenueAsync();
            await Clients.All.SendAsync("ReceiveLastWeekRevenue", LastWeekRevenue);


        }
        public async Task DefaultStatistic()
        {
            var menuValues = await _productService.TGetProductListWithCategoryAsync();
            var menuResult = _mapper.Map<List<ResultProductWithCategoryDto>>(menuValues);
            await Clients.Caller.SendAsync("ReceiveMenuStatistic", menuResult);

            var offerValues = await _discountService.TGetAllAsync();
            var offerResult = _mapper.Map<List<ResultDiscountDto>>(offerValues);
            await Clients.Caller.SendAsync("ReceiveDiscount", offerResult);

            var value = await _productService.TGetMostExpensiveBurgerAsync();
            var result = _mapper.Map<MaxPriceHamburgerDto>(value);
            await Clients.Caller.SendAsync("ReceiveSliderBurger", result);
        }
    }
}