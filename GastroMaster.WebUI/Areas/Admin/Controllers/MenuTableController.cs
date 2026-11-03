using GastroMaster.DtoLayer.DTOS.MenuTableDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuTableController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuTableController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateMenuTable()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuTable(CreateMenuTableDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("MenuTable", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMenuTable(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"MenuTable/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateMenuTableDto>();
                return View(value);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> UpdateMenuTable(UpdateMenuTableDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("MenuTable",dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMenuTable(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"MenuTable/{id}");
            return RedirectToAction(nameof(Index));
        }

    }
}
