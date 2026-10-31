using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.ViewComponents.AdminLayout
{
    public class AdminLayoutHeaderViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminLayoutHeaderViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Email = user.Email;
            return View(user);
        }
    }
}
