using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        public IActionResult Index(int? tableId)
        {
            if (tableId != null)
            {
                Response.Cookies.Append("TableId", tableId.ToString(), new CookieOptions
                {
                    HttpOnly = true,                  
                    Secure = true,                         
                    SameSite = SameSiteMode.Lax,          
                    Expires = DateTime.UtcNow.AddHours(3)   
                });
            }

            return View();
        }
    }
}
