using GastroMaster.DtoLayer.DTOS.FeatureDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7139/api/Feature");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFeatureDto>>();
                return View(values);
            }
            return View(new List<ResultFeatureDto>());
        }

        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync("https://localhost:7139/api/Feature", createFeatureDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(createFeatureDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7139/api/Feature/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureDto>();
                return View(value);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync("https://localhost:7139/api/Feature", updateFeatureDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(updateFeatureDto);
        }

        public async Task<IActionResult> DeleteFeature(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7139/api/Feature/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}