using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.DiscountDtos;
using GastroMaster.DtoLayer.DTOS.ValidationDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DiscountController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DiscountController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync("Discount");
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultDiscountDto>>();
                return View(values);
            }
            return View(new List<ResultDiscountDto>());
        }
        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var discounts = await client.GetFromJsonAsync<List<ResultDiscountDto>>("Discount");


            using(var workBook=new XLWorkbook())
            {
                var worksheet = workBook.Worksheets.Add("İndirimler");

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Başlık";
                worksheet.Cell(1, 3).Value = "Fiyat";
                worksheet.Cell(1, 4).Value = "Açıklama";
                worksheet.Cell(1, 5).Value = "Resim";
                worksheet.Cell(1, 6).Value = "Durum";

                var headerRow = worksheet.Row(1);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;

                foreach(var item in discounts)
                {
                    worksheet.Cell(row,1).Value=item.DiscountId;
                    worksheet.Cell(row, 2).Value = item.Title;
                    worksheet.Cell(row, 3).Value = item.Amount;
                    worksheet.Cell(row, 4).Value = item.Description;
                    worksheet.Cell(row, 5).Value = item.ImageUrl;
                    worksheet.Cell(row, 1).Value = item.Status;
                    row++;
                }
                worksheet.Columns().AdjustToContents();

                worksheet.SheetView.FreezeRows(1);
                using (var stream=new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    
                    var content= stream.ToArray();

                    return File(
               content,
               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",    // Excel dosyasının standart MIME tipi
               $"İndirimler{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
               );
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> ChangeStatusToTrue(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.PutAsync($"https://localhost:7139/api/Discount/ChangeStatusToTrue/{id}", null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatusToFalse(int id)
        {
            var client = httpClientFactory.CreateClient();
            await client.PutAsync($"https://localhost:7139/api/Discount/ChangeStatusToFalse/{id}", null);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("Discount", createDiscountDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(createDiscountDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDiscount(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"Discount/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateDiscountDto>();
                return View(value);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("Discount", updateDiscountDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ValidationErrorDto>>();
            foreach (var item in values)
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            return View(updateDiscountDto);
        }

        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var client = httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"Discount/{id}");
            return RedirectToAction("Index");
        }
    }
}