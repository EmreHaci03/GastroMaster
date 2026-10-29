using GastroMaster.DtoLayer.DTOS.BasketDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    public class BasketsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasketsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Basket/GetBasketByMenuTableId/4");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultBasketWithDetailsDto>>();
                return View(values);
            }
            return View(new List<ResultBasketWithDetailsDto>());
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddBasket(int productId)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");

            var productResponse = await client.GetAsync($"https://localhost:7139/api/Product/{productId}");
            if (!productResponse.IsSuccessStatusCode)
                return BadRequest("Ürün alınamadı: " + (int)productResponse.StatusCode);


            var product = await productResponse.Content.ReadFromJsonAsync<GetProductByIdDto>();


            var dto = new CreateBasketDto()
            {
                Count = 1,
                MenuTableId = 4,
                Price = product.Price,
                ProductId = productId
            };

            var responseMessage = await client.PostAsJsonAsync("Basket", dto);
            if(responseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest("Sepete eklenemedi: " + await responseMessage.Content.ReadAsStringAsync());
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.DeleteAsync($"Basket/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
