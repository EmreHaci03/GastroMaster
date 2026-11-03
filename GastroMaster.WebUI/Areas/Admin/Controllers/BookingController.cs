using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.BookingDtos;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BookingController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var booking = await client.GetFromJsonAsync<List<ResultBookingDto>>("Booking");


            using (var workBook = new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("Rezervasyonlar");

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Ad Soyad";
                worksheet.Cell(1, 3).Value = "Telefon";
                worksheet.Cell(1, 4).Value = "Mail";
                worksheet.Cell(1, 5).Value = "Kişi Sayısı";
                worksheet.Cell(1, 6).Value = "Tarih";
                worksheet.Cell(1, 7).Value = "Durum";
                worksheet.Cell(1, 8).Value = "Açıklama";

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach(var item in booking)
                {
                    worksheet.Cell(row, 1).Value = item.BookingId;
                    worksheet.Cell(row, 2).Value = item.Name;
                    worksheet.Cell(row, 3).Value = item.Phone;
                    worksheet.Cell(row, 4).Value = item.Mail;
                    worksheet.Cell(row, 5).Value = item.PersonCount;
                    worksheet.Cell(row, 6).Value = item.Date;
                    worksheet.Cell(row, 7).Value = item.Status;
                    worksheet.Cell(row, 8).Value = item.Description;
                }

                worksheet.Columns().AdjustToContents();  //Sütun genişliklerini otomatik ayarlıyoruz

                worksheet.SheetView.FreezeRows(1);  //    kullanıcı aşağı kaydırsa bile başlıklar üstte sabit kalır

                using(var stream=new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content=stream.ToArray();

                    return File(
                  content,
                  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",    // Excel dosyasının standart MIME tipi
                  $"Rezervasyonlar{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                  );
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> SendBookingMail(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var booking = await client.GetFromJsonAsync<ResultBookingDto>($"Booking/{id}");

            var mailSettings = _configuration.GetSection("MailSettings");

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(mailSettings["SenderName"], mailSettings["SenderMail"]));
            mimeMessage.To.Add(new MailboxAddress(booking.Name, booking.Mail));
            mimeMessage.Subject = "Rezervasyonunuz Onaylandı - GastroMaster";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
    <div style='font-family:Arial,sans-serif; max-width:520px; margin:auto; border:1px solid #eee; border-radius:12px; overflow:hidden;'>
        <div style='background:#1f1f1f; color:#f2b705; padding:24px; text-align:center;'>
            <h2 style='margin:0;'>GastroMaster</h2>
        </div>
        <div style='padding:28px; color:#333;'>
            <p>Merhaba <strong>{System.Net.WebUtility.HtmlEncode(booking.Name)}</strong>,</p>
            <p>Rezervasyonunuz onaylandı. Sizi ağırlamaktan mutluluk duyacağız.</p>
            <table style='width:100%; margin:20px 0; border-collapse:collapse;'>
                <tr><td style='padding:8px 0; color:#888; width:130px;'>Rezervasyon No</td><td style='padding:8px 0;'><strong>#{booking.BookingId}</strong></td></tr>
                <tr><td style='padding:8px 0; color:#888;'>Ad Soyad</td><td style='padding:8px 0;'><strong>{System.Net.WebUtility.HtmlEncode(booking.Name)}</strong></td></tr>
                <tr><td style='padding:8px 0; color:#888;'>Telefon</td><td style='padding:8px 0;'><strong>{System.Net.WebUtility.HtmlEncode(booking.Phone)}</strong></td></tr>
                <tr><td style='padding:8px 0; color:#888;'>E-posta</td><td style='padding:8px 0;'><strong>{System.Net.WebUtility.HtmlEncode(booking.Mail)}</strong></td></tr>
                <tr><td style='padding:8px 0; color:#888;'>Tarih</td><td style='padding:8px 0;'><strong>{booking.Date:dd.MM.yyyy HH:mm}</strong></td></tr>
                <tr><td style='padding:8px 0; color:#888;'>Kişi Sayısı</td><td style='padding:8px 0;'><strong>{booking.PersonCount}</strong></td></tr>
                <tr><td style='padding:8px 0; color:#888;'>Not</td><td style='padding:8px 0;'><strong>{(string.IsNullOrWhiteSpace(booking.Description) ? "-" : System.Net.WebUtility.HtmlEncode(booking.Description))}</strong></td></tr>
                <tr><td style='padding:8px 0; color:#888;'>Durum</td><td style='padding:8px 0;'><span style='background:#e8f8ee; color:#1d8a4e; padding:4px 12px; border-radius:20px; font-weight:bold; font-size:13px;'>{System.Net.WebUtility.HtmlEncode(booking.Status)}</span></td></tr>
            </table>
            <p style='color:#888; font-size:13px;'>Değişiklik için bizi 0216 345 67 89 numarasından arayabilirsiniz.</p>
        </div>
    </div>";
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(mailSettings["Host"], int.Parse(mailSettings["Port"]), SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(mailSettings["SenderMail"], mailSettings["Password"]);
                await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
            }

            TempData["BookingMessage"] = $"{booking.Name} adlı müşteriye onay maili gönderildi";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Booking/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BookingStatusApproved(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Booking/BookingStatusApproved/{id}", null);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BookingStatusCancelled(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Booking/BookingStatusCancelled/{id}", null);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> BookingStatusPending(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.PutAsync($"Booking/BookingStatusPending/{id}", null);
            return RedirectToAction(nameof(Index));
        }
    }
}