using GastroMaster.DtoLayer.DTOS.AboutDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("About");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
                return View(values);
            }
            return View(new List<ResultAboutDto>());
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("About", createAboutDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

            return View(createAboutDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"About/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateAboutDto>();
                return View(value);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("About", updateAboutDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

            return View(updateAboutDto);
        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"About/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}