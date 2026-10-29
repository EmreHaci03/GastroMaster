using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class FeedBacksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeedBacksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Feedback/MarkAsRead/{id}", null);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Feedback/MarkAsUnread/{id}", null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeedBack(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.DeleteAsync($"FeedBack/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Geri Bildirim Başarıyla Silindi";
                return RedirectToAction(nameof(Index));
            }

            return View();

        }
    }
}
