using GastroMaster.DtoLayer.DTOS.SliderDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Slider");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSliderDto>>();
                return View(values);
            }
            return View(new List<ResultSliderDto>());
        }

        [HttpGet]
        public ActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Slider", dto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.DeleteAsync($"Slider/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"Slider/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<UpdateSliderDto>();
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("Slider", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(dto);
        }
    }
}
