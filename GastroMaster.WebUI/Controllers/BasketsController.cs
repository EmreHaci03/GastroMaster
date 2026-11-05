using GastroMaster.DtoLayer.DTOS.BasketDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    [AllowAnonymous]
    public class BasketsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasketsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Cookie'deki masa numarasını okur, yoksa null döner
        private int? GetTableId()
        {
            if (int.TryParse(Request.Cookies["TableId"], out int tableId))
                return tableId;

            return null;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tableId = GetTableId();

            // QR okutulmamışsa masa yok, boş sepet göster
            if (tableId == null)
                return View(new List<ResultBasketWithDetailsDto>());

            ViewBag.TableId = tableId;

            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"Basket/GetBasketByMenuTableId/{tableId}");   // 4 yerine cookie'deki masa
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
            var tableId = GetTableId();

            // QR okutulmadan sepete eklenemez
            if (tableId == null)
                return BadRequest("Lütfen masadaki QR kodu okutun.");

            var client = _httpClientFactory.CreateClient("GastroMasterApi");

            var productResponse = await client.GetAsync($"https://localhost:7139/api/Product/{productId}");
            if (!productResponse.IsSuccessStatusCode)
                return BadRequest("Ürün alınamadı: " + (int)productResponse.StatusCode);

            var product = await productResponse.Content.ReadFromJsonAsync<GetProductByIdDto>();

            var dto = new CreateBasketDto()
            {
                Count = 1,
                MenuTableId = tableId.Value,   // 4 yerine cookie'deki masa
                Price = product.Price,
                ProductId = productId
            };

            var responseMessage = await client.PostAsJsonAsync("Basket", dto);
            if (responseMessage.IsSuccessStatusCode)
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