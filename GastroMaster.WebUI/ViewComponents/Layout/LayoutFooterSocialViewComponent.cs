using GastroMaster.DtoLayer.DTOS.SocialMediaDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GastroMaster.WebUI.ViewComponents.Layout
{
    public class LayoutFooterSocialViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LayoutFooterSocialViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("SocialMedia");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSocialMediaDto>>();
                return View(values);
            }
            return View(new List<ResultSocialMediaDto>());
        }
    }
}
