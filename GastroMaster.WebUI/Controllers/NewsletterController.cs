using GastroMaster.DtoLayer.DTOS.NewsletterDtos;
using Microsoft.AspNetCore.Mvc;


namespace GastroMaster.WebUI.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NewsletterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]

        public async Task<IActionResult> CreateNewsletter(CreateNewsletterDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Newsletter", dto);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Bültene Abone Olundu";
                return RedirectToAction("Index", "Default");
            }
            TempData["ErrorMessage"] = "Bültene Abone Olurken Hata Oluştu";
            return RedirectToAction("Index", "Default");
        }
    }
}
