using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.FeedBackDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
