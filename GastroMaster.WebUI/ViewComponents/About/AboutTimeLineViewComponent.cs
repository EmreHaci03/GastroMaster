using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.About
{
    public class AboutTimeLineViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
