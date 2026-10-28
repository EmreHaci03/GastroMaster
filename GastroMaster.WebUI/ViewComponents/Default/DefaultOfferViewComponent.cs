using GastroMaster.DtoLayer.DTOS.DiscountDtos;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.ViewComponents.Default
{
    public class DefaultOfferViewComponent:ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
