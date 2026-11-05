using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.FeedBackDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class FeedBacksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeedBacksController(IHttpClientFactory httpClientFactory)
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
            var values = await client.GetFromJsonAsync<List<ResultFeedBackDto>>("Feedback");
            values = values.OrderByDescending(x => x.SendDate).ToList();

            var unreadCount = values.Count(x => !x.IsRead);
            var suggestionCount = values.Count(x => x.Subject == "Öneri");
            var complaintCount = values.Count(x => x.Subject == "Şikayet");

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
                        column.Item().Text("Geri Bildirim Raporu").FontSize(14).SemiBold();
                        column.Item().Text($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm} · Toplam {values.Count} geri bildirim · {unreadCount} okunmamış · {suggestionCount} öneri · {complaintCount} şikayet").FontColor(Colors.Grey.Darken1);
                        column.Item().PaddingVertical(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.ConstantColumn(60);
                            columns.RelativeColumn(6);
                            columns.ConstantColumn(85);
                            columns.ConstantColumn(65);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#1572e8").Padding(6).Text("#").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Ad Soyad").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("E-posta").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Konu").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Mesaj").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Tarih").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Durum").FontColor(Colors.White).Bold();
                        });

                        int count = 0;
                        foreach (var item in values)
                        {
                            count++;
                            var background = count % 2 == 0 ? Colors.Grey.Lighten4 : Colors.White;

                            var subjectColor = item.Subject switch
                            {
                                "Şikayet" => "#e63946",
                                "Öneri" => "#c48a00",
                                "İstek" => "#1572e8",
                                _ => "#616161"
                            };

                            table.Cell().Background(background).Padding(6).Text(count.ToString());
                            table.Cell().Background(background).Padding(6).Text(item.NameSurname).SemiBold();
                            table.Cell().Background(background).Padding(6).Text(item.Mail).FontColor(Colors.Grey.Darken2);
                            table.Cell().Background(background).Padding(6).Text(item.Subject).FontColor(subjectColor).SemiBold();
                            table.Cell().Background(background).Padding(6).Text(item.Content).FontColor(Colors.Grey.Darken3);
                            table.Cell().Background(background).Padding(6).Text(item.SendDate.ToString("dd.MM.yyyy HH:mm"));
                            table.Cell().Background(background).Padding(6).Text(item.IsRead ? "Okundu" : "Okunmadı")
                                .FontColor(item.IsRead ? "#2bb930" : "#1572e8").SemiBold();
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

            var fileName = $"GeriBildirimler-{DateTime.Now:dd-MM-yyyy}.pdf";
            return File(pdf, "application/pdf", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var feedBack = await client.GetFromJsonAsync<List<ResultFeedBackDto>>("FeedBack");

            using(var workBook=new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Geri Bildirimler");

                worksheet.Cell(1, 1).Value="ID";
                worksheet.Cell(1, 2).Value = "Ad Soyad";
                worksheet.Cell(1, 3).Value = "Mail";
                worksheet.Cell(1, 4).Value = "Konu";
                worksheet.Cell(1, 5).Value = "İçerik";
                worksheet.Cell(1, 6).Value = "Tarih";
                worksheet.Cell(1, 7).Value = "Durum";

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;
                int row = 2;
                foreach(var item in feedBack)
                {
                    worksheet.Cell(row, 1).Value = item.FeedbackId;
                    worksheet.Cell(row, 2).Value = item.NameSurname;
                    worksheet.Cell(row, 3).Value = item.Mail;
                    worksheet.Cell(row, 4).Value = item.Subject;
                    worksheet.Cell(row, 5).Value = item.Content;
                    worksheet.Cell(row, 6).Value = item.SendDate;
                    worksheet.Cell(row, 7).Value = item.IsRead;
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
                   $"Geri Bildirimler{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                   );
                }

            }
        }
        [HttpGet]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Feedback/MarkAsRead/{id}", null);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Feedback/MarkAsUnread/{id}", null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeedBack(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.DeleteAsync($"FeedBack/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Geri Bildirim Başarıyla Silindi";
                return RedirectToAction(nameof(Index));
            }

            return View();

        }
    }
}
