using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.CategoryDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
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
        public async Task<IActionResult> ExportToExcel()
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var categories = await client.GetFromJsonAsync<List<ResultCategoryDto>>("Category");


            using(var workBook=new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Kategoriler");

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Kategori Adı";
                worksheet.Cell(1, 2).Value = "Kategori Durum";

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;

                foreach(var item in categories)
                {
                    worksheet.Cell(row, 1).Value = item.CategoryId;
                    worksheet.Cell(row, 2).Value = item.CategoryName;
                    worksheet.Cell(row, 3).Value = item.CategoryStatus;
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                worksheet.SheetView.FreezeRows(1);

                using(var stream=new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content = stream.ToArray();

                    return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",    // Excel dosyasının standart MIME tipi
                $"Kategoriler{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                );
                }

            }
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