using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.Layout
{
    public class LayoutHeadViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
