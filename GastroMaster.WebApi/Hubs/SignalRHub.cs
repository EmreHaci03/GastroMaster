using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.BookingDtos;
using GastroMaster.DtoLayer.DTOS.DiscountDtos;
using GastroMaster.DtoLayer.DTOS.FeedBackDtos;
using GastroMaster.DtoLayer.DTOS.MenuTableDtos;
using GastroMaster.DtoLayer.DTOS.NewsletterDtos;
using GastroMaster.DtoLayer.DTOS.NotificationDtos;
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
        private readonly INotificationService _notificationService;
        private readonly IBookingService _bookingService;
        private readonly INewsletterService _newsletterService;
        private readonly IFeedBackService _feedBackService;
        private readonly IMapper _mapper;
        public SignalRHub(IStatisticService statisticService, ICashRegisterService cashRegisterService, IOrderService orderService, IMenuTableService menuTableService, IOrderDetailService orderDetailService, IMapper mapper, IProductService productService, IDiscountService discountService, INotificationService notificationService, IBookingService bookingService, INewsletterService newsletterService, IFeedBackService feedBackService)
        {
            _statisticService = statisticService;
            _cashRegisterService = cashRegisterService;
            _orderService = orderService;
            _menuTableService = menuTableService;
            _orderDetailService = orderDetailService;
            _mapper = mapper;
            _productService = productService;
            _discountService = discountService;
            _notificationService = notificationService;
            _bookingService = bookingService;
            _newsletterService = newsletterService;
            _feedBackService = feedBackService;
        }

        public async Task SendStatistics()
        {
            var statistic = await _statisticService.TGetStatisticModelsAsync();
            await Clients.Caller.SendAsync("ReceiveStatistics", statistic);

            var orderStatistic = await _statisticService.TGetOrderCountByStatus(); 
            await Clients.Caller.SendAsync("ReceiveOrderStatistic", orderStatistic);

            var lastOrder = await _statisticService.TGetLastOrderAsync();
            await Clients.Caller.SendAsync("ReceiveLastOrder", lastOrder); 

            var hamburgerAveragePrice = await _statisticService.TGetAverageHamburgerPrice();
            await Clients.Caller.SendAsync("ReceiveHamburgerAveragePrice", hamburgerAveragePrice);

            var mostProductByCategory = await _statisticService.TProductCountByCategoryAsync();
            await Clients.Caller.SendAsync("ReceiveMostProductByCategory", mostProductByCategory);

            var cheapestProduct = await _statisticService.TGetCheapestProductAsync();
            await Clients.Caller.SendAsync("ReceiveCheapestProduct", cheapestProduct);

            var mostExpensiveProduct = await _statisticService.TReceiveMostExpensiveProduct();
            await Clients.Caller.SendAsync("ReceiveMostExpensiveProduct", mostExpensiveProduct);

            var CashRegisterTotalAmount = await _cashRegisterService.TGetTotalAmountAsync();
            await Clients.Caller.SendAsync("ReceiveCashRegisterTotalAmount", CashRegisterTotalAmount);

            var TodayOrderAmount = await _orderService.TGetTodayRevenueAsync();
            await Clients.Caller.SendAsync("ReceiveTodayOrderAmount", TodayOrderAmount);

            var MenuTableCount = await _menuTableService.TMenuTableCount();
            await Clients.Caller.SendAsync("ReceiveMenuTableCount", MenuTableCount);

            var ActiveMenuTableCount = await _menuTableService.TActiveMenuTableCount();
            await Clients.Caller.SendAsync("ReceiveActiveMenuTableCount", ActiveMenuTableCount);
        }

        public async Task DashboardStatistic()
        {
            var todayRevenue = await _orderService.TGetTodayRevenueAsync();
            await Clients.Caller.SendAsync("ReceiveTodayOrderAmount",todayRevenue);

            var TotalCashRegisterAmount = await _cashRegisterService.TGetTotalAmountAsync();
            await Clients.Caller.SendAsync("ReceiveCashRegisterAmount", TotalCashRegisterAmount);

            var GetClosedOrderCount = await _orderService.TGetClosedOrderCountAsync();
            await Clients.Caller.SendAsync("ReceiveClosedOrderCount", GetClosedOrderCount);

            var ActiveTableCount = await _menuTableService.TActiveMenuTableCount();
            await Clients.Caller.SendAsync("ReceiveActiveTableCount", ActiveTableCount);

            var occupiedMenuTableCount = await _orderService.TGetOccupiedMenuTableCountAsync();
            await Clients.Caller.SendAsync("ReceiveOccupiedMenuTableCount", occupiedMenuTableCount);

            var TopSellingProducts=await _orderDetailService.TGetTopSellingProductsAsync();
            await Clients.Caller.SendAsync("ReceiveTopSellingProducts", TopSellingProducts);

            var LastOrders = await _orderDetailService.TGetLastOrders();
            await Clients.Caller.SendAsync("ReceiveLastOrders", LastOrders);

            var LastWeekRevenue = await _orderService.TGetLastWeekDailyRevenueAsync();
            await Clients.Caller.SendAsync("ReceiveLastWeekRevenue", LastWeekRevenue);

            var TodayReservationCount = await _bookingService.TGetTodayReservationCount();
            await Clients.Caller.SendAsync("ReceiveTodayReservationCount", TodayReservationCount);
            var TotalReservationCount = await _bookingService.TGetTotalReservationCount();
            await Clients.Caller.SendAsync("ReceiveTotalReservationCount", TotalReservationCount);


            var MenuTableList = await _menuTableService.TGetMenuTableWithOrderStatus();
            var MenuTableResult=_mapper.Map<List<GetMenuTableWithOrderStatusDto>>(MenuTableList);
            await Clients.Caller.SendAsync("ReceiveMenuTableListWithStatus", MenuTableResult);


        }
        public async Task DefaultStatistic()
        {
            var menuValues = await _productService.TGetProductListWithCategoryAsync();
            var menuResult = _mapper.Map<List<ResultProductWithCategoryDto>>(menuValues);
            await Clients.Caller.SendAsync("ReceiveMenuStatistic", menuResult);

            var offerValues = await _discountService.TGetStatusTrueDiscountList();
            var offerResult = _mapper.Map<List<ResultDiscountDto>>(offerValues);
            await Clients.Caller.SendAsync("ReceiveTrueStatusDiscount", offerResult);

            var value = await _productService.TGetMostExpensiveBurgerAsync();
            var result = _mapper.Map<MaxPriceHamburgerDto>(value);
            await Clients.Caller.SendAsync("ReceiveSliderBurger", result);

        }
        public async Task SendNotification()
        {
            var UnreadNotificationCount = await _notificationService.TNotificationCountByStatusFalse();
            await Clients.Caller.SendAsync("ReceiveUnreadNotificationCount", UnreadNotificationCount);

            var unReadNotifications=await _notificationService.TGetNotificationListByFalseStatus();
            var result = _mapper.Map<List<ResultNotificationDto>>(unReadNotifications);
            await Clients.Caller.SendAsync("ReceiveUnReadNotificationListByFalse", result);

            var NotificationList=await _notificationService.TGetAllAsync();
            var resultNotificationList = _mapper.Map<List<ResultNotificationDto>>(NotificationList);
            await Clients.Caller.SendAsync("ReceiveNotificationList",resultNotificationList);
        }

        public async Task SendBooking()
        {
            var bookingList=await _bookingService.TGetAllAsync();
            var result = _mapper.Map<List<ResultBookingDto>>(bookingList);
            await Clients.Caller.SendAsync("ReceiveBookingList", result);
 
        }
        public async Task SendNewsletter()
        {
            var newsletterList = await _newsletterService.TGetAllAsync();
            var result=_mapper.Map<List<ResultNewsletterDto>>(newsletterList);
            await Clients.Caller.SendAsync("ReceiveNewsletterList", result);
        }

        public async Task SendMenuTable()
        {
            var MenuTableWithOrderStatus=await _menuTableService.TGetMenuTableWithOrderStatus();
            var result=_mapper.Map<List<GetMenuTableWithOrderStatusDto>>(MenuTableWithOrderStatus);
            await Clients.Caller.SendAsync("ReceiveMenuTableWithOrderStatus", result);
        }
        private static int Client; // static alan nesneye değil sınıfa aittir. Kaç hub nesnesi oluşturulursa oluşturulsun, hepsi aynı tek Client değişkenini kullanır.

        public override async Task OnConnectedAsync()
        {
            Client++;
            await Clients.All.SendAsync("ReceiveClientCount", Client);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Client--;
            await Clients.All.SendAsync("ReceiveClientCount", Client);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",user,message);
        }

        public async Task SendFeedBack()
        {
            var FeedBackList = await _feedBackService.TGetAllAsync();
            var result = _mapper.Map<List<ResultFeedBackDto>>(FeedBackList);
            await Clients.Caller.SendAsync("ReceiveFeedBack", result);
        }
    }
}