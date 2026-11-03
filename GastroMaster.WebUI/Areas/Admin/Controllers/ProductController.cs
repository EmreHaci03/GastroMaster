using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GastroMaster.DtoLayer.DTOS.CategoryDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Product/GetProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
                return View(values);
            }
            return View(new List<ResultProductWithCategoryDto>());
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var product = await client.GetFromJsonAsync<List<ResultProductWithCategoryDto>>("Product/GetProductListWithCategory/");

            using(var workBook=new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Ürünler");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Ürün";
                worksheet.Cell(1, 3).Value = "Açıklama";
                worksheet.Cell(1, 4).Value = "Fiyat";
                worksheet.Cell(1, 5).Value = "Resim";
                worksheet.Cell(1, 6).Value = "Durum";
                worksheet.Cell(1, 7).Value = "Kategori";

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var item in product)
                {
                    worksheet.Cell(row,1).Value = item.ProductId;
                    worksheet.Cell(row, 2).Value = item.ProductName;
                    worksheet.Cell(row, 3).Value = item.Description;
                    worksheet.Cell(row, 4).Value = item.Price;
                    worksheet.Cell(row, 5).Value = item.ImageUrl;
                    worksheet.Cell(row, 6).Value = item.ProductStatus;
                    worksheet.Cell(row, 7).Value = item.Category;
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
                   $"Ürünler{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                   );
                }
            }

        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client= _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>() ?? new();
                ViewBag.Categories = values;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var client=_httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Product", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var categoryResponseMessage = await client.GetAsync("Category");
            if (categoryResponseMessage.IsSuccessStatusCode)
            {
                var categories = await categoryResponseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>() ?? new();
                ViewBag.Categories = categories;
            }

            var responseMessage = await client.GetAsync($"Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
                if (value != null)
                    return View(value);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("Product", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client= _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.DeleteAsync($"Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
