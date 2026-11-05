using GastroMaster.DtoLayer.DTOS.MenuTableDtos;
using GastroMaster.DtoLayer.DTOS.OrderDetailDtos;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuTableController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuTableController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult QrCode(int id)
        {
            var url = $"{Request.Scheme}://{Request.Host}/Default/Index?tableId={id}";

            using (var qrGenerator = new QRCodeGenerator())
            {
                using(var qrCodeData=qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q))
                {
                    var qrCode = new PngByteQRCode(qrCodeData);

                    var qrBytes = qrCode.GetGraphic(20);

                    return File(qrBytes, "image/png", $"Masa-{id}-QR.png");
                }
            }            
        }
        [HttpGet]
        public async Task<IActionResult> GetTableOrders(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"OrderDetail/GetOrdersByMenuTable/{id}");
            if (!responseMessage.IsSuccessStatusCode)
                return Json(new List<GetOrderDetailWithDetailDto>());

            var values = await responseMessage.Content.ReadFromJsonAsync<List<GetOrderDetailWithDetailDto>>();
            return Json(values);
        }

        [HttpGet]
        public IActionResult CreateMenuTable()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuTable(CreateMenuTableDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PostAsJsonAsync("MenuTable", dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMenuTable(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.GetAsync($"MenuTable/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var value = await responseMessage.Content.ReadFromJsonAsync<UpdateMenuTableDto>();
                return View(value);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> UpdateMenuTable(UpdateMenuTableDto dto)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            var responseMessage = await client.PutAsJsonAsync("MenuTable",dto);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMenuTable(int id)
        {
            var client = _httpClientFactory.CreateClient("GastroMasterApi");
            await client.DeleteAsync($"MenuTable/{id}");
            return RedirectToAction(nameof(Index));
        }

    }
}
