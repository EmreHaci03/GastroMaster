using GastroMaster.DtoLayer.DTOS.TimeLineDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TimeLineController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TimeLineController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Timeline");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultTimeLineDto>>();
                return View(values);
            }
            return View(new List<ResultTimeLineDto>());
        }

        [HttpGet]
        public IActionResult CreateTimeLine()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeLine(CreateTimeLineDto createTimelineDto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Timeline", createTimelineDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
                foreach (var item in values)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            else
            {
                ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin");
            }
            return View(createTimelineDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTimeLine(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"Timeline/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateTimeLineDto>();
                return View(value);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTimeLine(UpdateTimeLineDto updateTimelineDto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("Timeline", updateTimelineDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
                foreach (var item in values)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            else
            {
                ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin");
            }
            return View(updateTimelineDto);
        }

        public async Task<IActionResult> DeleteTimeLine(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Timeline/{id}");
            return RedirectToAction("Index");
        }
    }
}