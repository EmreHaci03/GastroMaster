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


            services.AddScoped<ITestimonialService,TestimonialManager>();
            services.AddScoped<IBookingService, BookingManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IDiscountService, DiscountManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ISocialMediaService, SocialMediaManager>();
            services.AddScoped<ITestimonialService, TestimonialManager>();

            services.AddAutoMapper(cfg => { }, typeof(GeneralMapping));

            return services;
        }
    }
}
