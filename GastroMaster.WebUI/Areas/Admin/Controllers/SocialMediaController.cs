using GastroMaster.DtoLayer.DTOS.SocialMediaDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SocialMediaController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7139/api/SocialMedia");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSocialMediaDto>>();
                return View(values);
            }
            return View(new List<ResultSocialMediaDto>());
        }

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.PostAsJsonAsync("https://localhost:7139/api/SocialMedia", createSocialMediaDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(createSocialMediaDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7139/api/SocialMedia/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateSocialMediaDto>();
                return View(value);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var client = httpClientFactory.CreateClient();
            var responseMessage = await client.PutAsJsonAsync("https://localhost:7139/api/SocialMedia", updateSocialMediaDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(updateSocialMediaDto);
        }

        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7139/api/SocialMedia/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}