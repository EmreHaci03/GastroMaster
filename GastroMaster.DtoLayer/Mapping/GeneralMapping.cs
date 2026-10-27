using AutoMapper;
using GastroMaster.DataAccessLayer.Models;
using GastroMaster.DtoLayer.DTOS.AboutDtos;
using GastroMaster.DtoLayer.DTOS.AboutItemDtos;
using GastroMaster.DtoLayer.DTOS.BookingDtos;
using GastroMaster.DtoLayer.DTOS.CashRegisterDtos;
using GastroMaster.DtoLayer.DTOS.CategoryDtos;
using GastroMaster.DtoLayer.DTOS.ContactDtos;
using GastroMaster.DtoLayer.DTOS.DashboardDtos;
using GastroMaster.DtoLayer.DTOS.DiscountDtos;
using GastroMaster.DtoLayer.DTOS.FeatureDtos;
using GastroMaster.DtoLayer.DTOS.MenuTableDtos;
using GastroMaster.DtoLayer.DTOS.NewsletterDtos;
using GastroMaster.DtoLayer.DTOS.NotificationDtos;
using GastroMaster.DtoLayer.DTOS.OrderDetailDtos;
using GastroMaster.DtoLayer.DTOS.OrderDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using GastroMaster.DtoLayer.DTOS.SliderDtos;
using GastroMaster.DtoLayer.DTOS.SocialMediaDtos;
using GastroMaster.DtoLayer.DTOS.StatisticDtos;
using GastroMaster.DtoLayer.DTOS.TestimonialDtos;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.DtoLayer.Mapping
{
    public class GeneralMapping : Profile
    {

        public GeneralMapping()
        {

            //About//
            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, GetAboutByIdDto>().ReverseMap();
            //About//


            //AboutItem//
            CreateMap<AboutItem, ResultAboutItemDto>().ReverseMap();
            CreateMap<AboutItem, CreateAboutItemDto>().ReverseMap();
            CreateMap<AboutItem, UpdateAboutItemDto>().ReverseMap();
            CreateMap<AboutItem, GetAboutItemByIdDto>().ReverseMap();
            //AboutItem//

            //Booking//
            CreateMap<Booking, ResultBookingDto>().ReverseMap();
            CreateMap<Booking, CreateBookingDto>().ReverseMap();
            CreateMap<Booking, UpdateBookingDto>().ReverseMap();
            CreateMap<Booking, GetBookingByIdDto>().ReverseMap();
            //Booking//

            //Notification//
            CreateMap<Notification, ResultNotificationDto>().ReverseMap();
            CreateMap<Notification, CreateNotificationDto>().ReverseMap();
            CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
            CreateMap<Notification, GetNotificationByIdDto>().ReverseMap();
            //Notification//

            //Category//
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
            //Category//

            //Contact//
            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, GetContactByIdDto>().ReverseMap();
            //Contact//

            //Slider//
            CreateMap<Slider, ResultSliderDto>().ReverseMap();
            CreateMap<Slider, CreateSliderDto>().ReverseMap();
            CreateMap<Slider, UpdateSliderDto>().ReverseMap();
            CreateMap<Slider, GetSliderByIdDto>().ReverseMap();
            //Slider//

            //Discount//
            CreateMap<Discount, ResultDiscountDto>().ReverseMap();
            CreateMap<Discount, CreateDiscountDto>().ReverseMap();
            CreateMap<Discount, UpdateDiscountDto>().ReverseMap();
            CreateMap<Discount, GetDiscountByIdDto>().ReverseMap();
            //Discount//

            //Feature//
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetFeatureByIdDto>().ReverseMap();
            //Feature//

            //Product//
            CreateMap<Product, ResultProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, GetProductByIdDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();
            CreateMap<Product, GetProductWithCategoryByIdDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();

            CreateMap<MaxPriceHamburgerModel,MaxPriceHamburgerDto>();
            //Product//

            //SocialMedia//
            CreateMap<SocialMedia, ResultSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, CreateSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, UpdateSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, GetSocialMediaByIdDto>().ReverseMap();
            //SocialMedia//

            //Testimonial//
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialByIdDto>().ReverseMap();
            //Testimonial//

            //Statistic//
            CreateMap<GetStatisticModel, GetStatisticDto>();
            CreateMap<ProductCountByCategoryModel, ProductCountByCategoryDto>();
            CreateMap<ProductCountByPriceRangeModel, ProductCountByPriceRangeDto>();
            CreateMap<ResultOrderModel, ResultOrderCountDto>();
            //Statistic//

            //Order//
            CreateMap<Order, ResultOrderDto>()
                .ForMember(dest => dest.MenuTable, opt => opt.MapFrom(src => src.MenuTable.Name))
                   .ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, GetOrderByIdDto>()
                .ReverseMap();
            CreateMap<Order, GetOrderWithDetailByIdDto>()
                .ForMember(dest => dest.MenuTableName, opt => opt.MapFrom(src => src.MenuTable.Name))
                .ReverseMap();

            //Order//

            //Order Detail//
            CreateMap<OrderDetail, ResultOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, UpdateOrderDetailDto>().ReverseMap();
            CreateMap<OrderDetail, GetOrderDetailByIdDto>().ReverseMap();
            CreateMap<OrderDetail, GetOrderDetailWithDetailDto>()
                  .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.ProductName))
                  .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order.MenuTable.Name))
                  .ReverseMap();
            CreateMap<OrderDetail, GetLastOrdersDto>()
             .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
             .ForMember(dest => dest.MenuTableName, opt => opt.MapFrom(src => src.Order.MenuTable.Name))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Order.Description))
             .ReverseMap();

            //Order Detail//

            //Cash Register//
            CreateMap<CashRegister, ResultCashRegisterDto>().ReverseMap();
            CreateMap<CashRegister, ReceiveCashRegisterTotalAmount>().ReverseMap();
            //Cash Register//


            //Cash Register//
            CreateMap<MenuTable, ResultMenuTableDto>().ReverseMap();
            CreateMap<MenuTable, CreateMenuTableDto>().ReverseMap();
            CreateMap<MenuTable, GetMenuTableByIdDto>().ReverseMap();
            CreateMap<MenuTable, UpdateMenuTableDto>().ReverseMap();
            //Cash Register//


            //Dashboard//
            CreateMap<GetLastOrderModel, GetLastOrdersDto>();
            CreateMap<TopSellingProductModels, TopSellingProductDto>();
            CreateMap<DailyRevenueModel, DailyRevenueDto>();
            //Dashboard//


            //Newsletter//
            CreateMap<Newsletter, CreateNewsletterDto>().ReverseMap();
            CreateMap<Newsletter, UpdateNewsletterDto>().ReverseMap();
            CreateMap<Newsletter, GetNewsletterByIdDto>().ReverseMap();
            CreateMap<Newsletter, ResultNewsletterDto>().ReverseMap();
            //Newsletter//
        }

    }
}