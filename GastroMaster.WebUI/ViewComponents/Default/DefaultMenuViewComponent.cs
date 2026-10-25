using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.Default
{
    public class DefaultMenuViewComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultMenuViewComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Product/GetProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
                var result = values.Where(x => x.ProductStatus).ToList();
                return View(result);
            }
            return View(new List<ResultProductWithCategoryDto>());
        }
    }
}
