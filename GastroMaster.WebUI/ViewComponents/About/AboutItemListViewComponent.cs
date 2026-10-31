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
            return View();
        }
    }
}
