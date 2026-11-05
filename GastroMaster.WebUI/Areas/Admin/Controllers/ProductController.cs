using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.CategoryDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

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
        public async Task<IActionResult> ExportToPdf()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var values = await client.GetFromJsonAsync<List<ResultProductWithCategoryDto>>("Product/GetProductListWithCategory");
            values = values.OrderBy(x => x.Category).ThenBy(x => x.ProductName).ToList();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(column =>
                    {
                        column.Item().Text("GastroMaster").FontSize(20).Bold().FontColor("#1572e8");
                        column.Item().Text("Ürün Raporu").FontSize(14).SemiBold();
                        column.Item().Text($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm} · Toplam {values.Count} ürün · Aktif {values.Count(x => x.ProductStatus)} · Ortalama fiyat ₺{(values.Any() ? values.Average(x => x.Price) : 0):N2}").FontColor(Colors.Grey.Darken1);
                        column.Item().PaddingVertical(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(5);
                            columns.ConstantColumn(60);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#1572e8").Padding(6).Text("#").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Ürün").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Kategori").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).AlignRight().Text("Fiyat").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Açıklama").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Durum").FontColor(Colors.White).Bold();
                        });

                        int count = 0;
                        foreach (var item in values)
                        {
                            count++;
                            var background = count % 2 == 0 ? Colors.Grey.Lighten4 : Colors.White;

                            table.Cell().Background(background).Padding(6).Text(count.ToString());
                            table.Cell().Background(background).Padding(6).Text(item.ProductName).SemiBold();
                            table.Cell().Background(background).Padding(6).Text(item.Category);
                            table.Cell().Background(background).Padding(6).AlignRight().Text($"₺{item.Price:N2}");
                            table.Cell().Background(background).Padding(6).Text(item.Description).FontColor(Colors.Grey.Darken2);
                            table.Cell().Background(background).Padding(6).Text(item.ProductStatus ? "Aktif" : "Pasif")
                                .FontColor(item.ProductStatus ? "#2bb930" : "#e63946").SemiBold();
                        }
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Sayfa ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
            }).GeneratePdf();

            var fileName = $"Urunler-{DateTime.Now:dd-MM-yyyy}.pdf";
            return File(pdf, "application/pdf", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var products = await client.GetFromJsonAsync<List<ResultProductWithCategoryDto>>("Product/GetProductListWithCategory");

            using (var workBook = new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Ürünler");

                worksheet.Cell(1, 1).Value = "#";
                worksheet.Cell(1, 2).Value = "Ürün";
                worksheet.Cell(1, 3).Value = "Kategori";
                worksheet.Cell(1, 4).Value = "Fiyat";
                worksheet.Cell(1, 5).Value = "Açıklama";
                worksheet.Cell(1, 6).Value = "Durum";

                var headerRow = worksheet.Range(1, 1, 1, 6);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var item in products.OrderBy(x => x.Category).ThenBy(x => x.ProductName))
                {
                    worksheet.Cell(row, 1).Value = row - 1;
                    worksheet.Cell(row, 2).Value = item.ProductName;
                    worksheet.Cell(row, 3).Value = item.Category;
                    worksheet.Cell(row, 4).Value = item.Price;
                    worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0.00 ₺";
                    worksheet.Cell(row, 5).Value = item.Description;
                    worksheet.Cell(row, 6).Value = item.ProductStatus ? "Aktif" : "Pasif";
                    row++;
                }

                worksheet.Columns().AdjustToContents();
                worksheet.Column(5).Width = 60;
                worksheet.Column(5).Style.Alignment.WrapText = true;
                worksheet.SheetView.FreezeRows(1);

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Urunler-{DateTime.Now:dd-MM-yyyy}.xlsx");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            await LoadCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Product", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            await AddErrors(responseMessage);
            await LoadCategories();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
                if (value != null)
                {
                    await LoadCategories();
                    return View(value);
                }
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

            await AddErrors(responseMessage);
            await LoadCategories();
            return View(dto);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Product/{id}");
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategories()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Category");
            ViewBag.Categories = responseMessage.IsSuccessStatusCode
                ? await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>() ?? new()
                : new List<ResultCategoryDto>();
        }

        private async Task AddErrors(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errors = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
                foreach (var item in errors)
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            else
            {
                ModelState.AddModelError("", "Bir hata oluştu, lütfen tekrar deneyin");
            }
        }
    }
}