using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.NewsletterDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> ExportToPdf()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var values = await client.GetFromJsonAsync<List<ResultNewsletterDto>>("Newsletter");
            values = values.OrderBy(x => x.Email).ToList();

            var gmailCount = values.Count(x => x.Email.EndsWith("@gmail.com"));
            var hotmailCount = values.Count(x => x.Email.EndsWith("@hotmail.com") || x.Email.EndsWith("@outlook.com"));
            var otherCount = values.Count - gmailCount - hotmailCount;

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(column =>
                    {
                        column.Item().Text("GastroMaster").FontSize(20).Bold().FontColor("#1572e8");
                        column.Item().Text("Bülten Aboneleri Raporu").FontSize(14).SemiBold();
                        column.Item().Text($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm} · Toplam {values.Count} abone · {gmailCount} Gmail · {hotmailCount} Hotmail/Outlook · {otherCount} diğer").FontColor(Colors.Grey.Darken1);
                        column.Item().PaddingVertical(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#1572e8").Padding(6).Text("#").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("E-posta").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Sağlayıcı").FontColor(Colors.White).Bold();
                        });

                        int count = 0;
                        foreach (var item in values)
                        {
                            count++;
                            var background = count % 2 == 0 ? Colors.Grey.Lighten4 : Colors.White;
                            var domain = item.Email.Contains("@") ? item.Email.Split("@")[1] : "-";

                            table.Cell().Background(background).Padding(6).Text(count.ToString());
                            table.Cell().Background(background).Padding(6).Text(item.Email).SemiBold();
                            table.Cell().Background(background).Padding(6).Text(domain).FontColor(Colors.Grey.Darken2);
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

            var fileName = $"BultenAboneleri-{DateTime.Now:dd-MM-yyyy}.pdf";
            return File(pdf, "application/pdf", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var notification = await client.GetFromJsonAsync<List<ResultNewsletterDto>>("Newsletter");

            using (var workBook = new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Bülten Aboneleri");

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Mail";

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach(var item in notification)
                {
                    worksheet.Cell(row, 1).Value=item.NewsletterId;
                    worksheet.Cell(row, 2).Value = item.Email;
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                worksheet.SheetView.FreezeRows(1);

                using(var stream=new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content=stream.ToArray();

                    return File(
                  content,
                  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",    // Excel dosyasının standart MIME tipi
                  $"Bülten Aboneleri{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                  );
                }

            }
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
