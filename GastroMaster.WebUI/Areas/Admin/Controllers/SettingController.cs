using GastroMaster.DtoLayer.DTOS.UserDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SettingController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index", "Default", new {area=""});
            UserEditDto dto = new UserEditDto();
            dto.Name = user.Name;
            dto.Surname= user.Surname;
            dto.Username = user.UserName;
            dto.Email = user.Email;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Name);
            if (user == null)
                return RedirectToAction("Index", "Default", new { area = "" });

            if (dto.Password == dto.ConfirmPassword)
            {
                user.Name = dto.Name;
                user.Surname= dto.Surname;
                user.UserName = dto.Username;
                user.Email= dto.Email;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
                var values = await _userManager.UpdateAsync(user);
                return RedirectToAction("LogOut", "Account", new {area=""});
            }

            return View();


        }
    }
}
