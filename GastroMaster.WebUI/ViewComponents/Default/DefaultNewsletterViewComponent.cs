using Microsoft.AspNetCore.Mvc;
namespace GastroMaster.WebUI.ViewComponents.Default
{
    public class DefaultNewsletterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
