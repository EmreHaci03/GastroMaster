using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.ViewComponents.AdminLayout
{
    public class AdminLayoutSideBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
