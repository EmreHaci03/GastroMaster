using GastroMaster.DtoLayer.DTOS.BookingDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BookingController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Booking/{id}");
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> BookingStatusApproved(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Booking/BookingStatusApproved/{id}", null);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BookingStatusCancelled(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Booking/BookingStatusCancelled/{id}", null);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BookingStatusPending(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Booking/BookingStatusPending/{id}", null);
            return RedirectToAction(nameof(Index));
        }
    }
}