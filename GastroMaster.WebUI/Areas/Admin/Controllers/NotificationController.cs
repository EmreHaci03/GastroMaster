using GastroMaster.DtoLayer.DTOS.NotificationDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NotificationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Notification/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> NotificationStatusToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsync($"Notification/ChangeNotificationStatusToTrue/{id}",null);
            if (responseMessage.IsSuccessStatusCode) {
                TempData["SuccessMessage"] = "Bildirim Okundu Olarak İşaretlendi";
                return RedirectToAction(nameof(Index));
             }
            TempData["ErrorMessage"] = "Hata Oluştu Tekrar Deneyiniz";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> NotificationStatusToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsync($"Notification/ChangeNotificationStatusToFalse/{id}", null);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Bildirim Okunmadı Olarak İşaretlendi";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Hata Oluştu Tekrar Deneyiniz";
            return View();
        }
    }
}