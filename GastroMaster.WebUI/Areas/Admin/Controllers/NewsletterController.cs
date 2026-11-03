using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.NewsletterDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
