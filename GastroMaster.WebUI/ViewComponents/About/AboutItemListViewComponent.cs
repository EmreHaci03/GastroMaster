using GastroMaster.DtoLayer.DTOS.AboutItemDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.About
{
    public class AboutItemListViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutItemListViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("AboutItem");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutItemDto>>();
                return View(values);
            }
            return View(new List<ResultAboutItemDto>());
        }
    }
}
