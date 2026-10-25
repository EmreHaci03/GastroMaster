using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
