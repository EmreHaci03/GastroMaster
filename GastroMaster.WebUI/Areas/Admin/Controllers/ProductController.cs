using GastroMaster.DtoLayer.DTOS.CategoryDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Product/GetProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
                return View(values);
            }
            return View(new List<ResultProductWithCategoryDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client= _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>() ?? new();
                ViewBag.Categories = values;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var client=_httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Product", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var categoryResponseMessage = await client.GetAsync("Category");
            if (categoryResponseMessage.IsSuccessStatusCode)
            {
                var categories = await categoryResponseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>() ?? new();
                ViewBag.Categories = categories;
            }

            var responseMessage = await client.GetAsync($"Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
                if (value != null)
                    return View(value);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("Product", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client= _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.DeleteAsync($"Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
