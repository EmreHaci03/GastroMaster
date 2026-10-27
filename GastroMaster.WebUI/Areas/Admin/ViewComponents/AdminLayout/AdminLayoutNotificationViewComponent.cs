using GastroMaster.DtoLayer.DTOS.NotificationDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.ViewComponents.AdminLayout
{
    public class AdminLayoutNotificationViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
