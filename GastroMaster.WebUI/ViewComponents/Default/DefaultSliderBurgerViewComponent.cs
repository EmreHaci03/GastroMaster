using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.Default
{
    public class DefaultSliderBurgerViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultSliderBurgerViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Product/GetMostExpensiveBurger");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<MaxPriceHamburgerDto>();
                return View(values);
            }
            return View(new MaxPriceHamburgerDto());
        }
    }
}
