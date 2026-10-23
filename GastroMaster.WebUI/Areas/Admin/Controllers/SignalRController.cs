using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SignalRController : Controller
    {
        public IActionResult Statistic()
        {
            return View();
        }
    }
}
