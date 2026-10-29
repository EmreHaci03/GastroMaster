using GastroMaster.DtoLayer.DTOS.FeedBackDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    [AllowAnonymous]
    public class FeedBackController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeedBackController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedBack(CreateFeedBackDto dto)
        {
            dto.NameSurname = User.Identity.Name;
            dto.SendDate = DateTime.Now;
            dto.IsRead = false;
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("FeedBack", dto);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["FeedbackMessage"] = "Geri Bildirim Gönderildi En Kısa Sürede Cevap Alacaksınız";
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }
    }
}
