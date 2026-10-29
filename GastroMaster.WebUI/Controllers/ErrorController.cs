using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("Error404");
                case 401:
                    return View("Error401");
                case 403:
                    return View("Error403");
                default:
                    return View("Error404");
            }
        }
    }
}
