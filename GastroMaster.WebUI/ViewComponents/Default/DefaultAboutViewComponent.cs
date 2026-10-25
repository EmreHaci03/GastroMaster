using GastroMaster.DtoLayer.DTOS.AboutDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.Default
{
    public class DefaultAboutViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultAboutViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("About");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
                return View(values);
            }
            return View(new List<ResultAboutDto>());
        }
    }
}
