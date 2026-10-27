using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.BusinessLayer.Concrete;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.EntityFramework;
using GastroMaster.DtoLayer.Mapping;

namespace GastroMaster.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ServiceRegistiration(this IServiceCollection services)
        {
            services.AddScoped<IAboutDal, EfAboutDal>();
            services.AddScoped<IBookingDal, EfBookingDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IContactDal, EfContactDal>();
            services.AddScoped<IDiscountDal, EfDiscountDal>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ISocialMediaDal, EfSocialMediaDal>();
            services.AddScoped<ITestimonialDal, EfTestimonialDal>();
            services.AddScoped<IFeatureDal, EfFeatureDal>();
            services.AddScoped<IStatisticDal, EfStatisticDal>();
            services.AddScoped<IOrderDal, EfOrderDal>();
            services.AddScoped<IOrderDetailDal, EfOrderDetailDal>();
            services.AddScoped<ICashRegisterDal, EfCashRegisterDal>();
            services.AddScoped<IMenuTableDal, EfMenuTableDal>();
            services.AddScoped<INewsletterDal, EfNewsletterDal>();
            services.AddScoped<IAboutItemDal, EfAboutItemDal>();
            services.AddScoped<ISliderDal, EfSliderDal>();
            services.AddScoped<INotificationDal, EfNotificationDal>();

            services.AddScoped<IStatisticService, StatisticManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDetailService, OrderDetailManager>();
            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<IFeatureService, FeatureManager>();
            services.AddScoped<ITestimonialService,TestimonialManager>();
            services.AddScoped<IBookingService, BookingManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IDiscountService, DiscountManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ISocialMediaService, SocialMediaManager>();
            services.AddScoped<ICashRegisterService, CashRegisterManager>();
            services.AddScoped<IMenuTableService, MenuTableManager>();
            services.AddScoped<INewsletterService, NewsletterManager>();
            services.AddScoped<IAboutItemService, AboutItemManager>();
            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<INotificationService, NotificationManager>();

            services.AddAutoMapper(cfg => { }, typeof(GeneralMapping));

            return services;
        }
    }
}
