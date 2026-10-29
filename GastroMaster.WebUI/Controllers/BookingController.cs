using GastroMaster.DtoLayer.DTOS.BookingDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    [AllowAnonymous]
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> CreateBooking()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            dto.Status = "Rezervasyon Alındı";
            var responseMessage = await client.PostAsJsonAsync("Booking", dto);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rezervasyon Başarıyla Yapıldı";
                return RedirectToAction("Index", "Default");
            }
            TempData["ErrorMessage"] = "Rezervasyon Sırasında Bir Hata Oluştu";
            return View(dto);
        }
    }
}
