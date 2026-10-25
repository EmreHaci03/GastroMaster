using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
