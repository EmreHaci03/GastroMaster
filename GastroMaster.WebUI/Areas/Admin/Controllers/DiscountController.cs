using GastroMaster.DtoLayer.DTOS.DiscountDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DiscountController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Discount");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultDiscountDto>>();
                return View(values);
            }
            return View(new List<ResultDiscountDto>());
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatusToTrue(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.PutAsync($"https://localhost:7139/api/Discount/ChangeStatusToTrue/{id}", null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatusToFalse(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.PutAsync($"https://localhost:7139/api/Discount/ChangeStatusToFalse/{id}", null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Discount", createDiscountDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(createDiscountDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDiscount(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"Discount/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateDiscountDto>();
                return View(value);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("Discount", updateDiscountDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(updateDiscountDto);
        }

        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Discount/{id}");
            return RedirectToAction("Index");
        }
    }
}