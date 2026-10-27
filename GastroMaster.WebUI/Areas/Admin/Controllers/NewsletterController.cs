using GastroMaster.DtoLayer.DTOS.NewsletterDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsletterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NewsletterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteNewsletter(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.DeleteAsync($"Newsletter/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
