using GastroMaster.DtoLayer.DTOS.ContactDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.Layout
{
    public class LayoutFooterViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LayoutFooterViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Contact");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultContactDto>>();
                return View(values);
            }
            return View(new List<ResultContactDto>());
        }
    }
}
