using GastroMaster.DtoLayer.DTOS.CategoryDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
                return View(values);
            }
            return View(new List<ResultCategoryDto>());
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Category", createCategoryDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(createCategoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateCategoryDto>();
                return View(value);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("Category", updateCategoryDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(updateCategoryDto);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Category/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}