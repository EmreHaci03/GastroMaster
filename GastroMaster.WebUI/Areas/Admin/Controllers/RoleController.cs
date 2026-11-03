using GastroMaster.DtoLayer.DTOS.AppRoleDtos;
using GastroMaster.DtoLayer.DTOS.UserDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var result = roles.Select(x => new ResultRoleDto
            {
                RoleId=x.Id,
                RoleName=x.Name,
            }).ToList();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserWithRolesDto>();

            foreach (var item in users)
            {
                result.Add(new UserWithRolesDto
                {
                    UserId = item.Id,
                    NameSurname = item.Name + " " + item.Surname,
                    UserName = item.UserName,
                    Email = item.Email,
                    Roles = (await _userManager.GetRolesAsync(item)).ToList()
                });
            }

            return View(result);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto dto)
        {
            AppRole appRole = new AppRole()
            {
                Name = dto.RoleName
            };

            var result = await _roleManager.CreateAsync(appRole);
            if(result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
            
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            var result = new UpdateRoleDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.RoleId);
            if(role == null)
                return NotFound();

            role.Name = dto.RoleName;

            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var allRoles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new RoleAssignDto
            {
                UserId = userId,
                UserName = user.Name,
                roleChecks = allRoles.Select(x => new RoleCheckDto
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    IsSelected = userRoles.Contains(x.Name)
                }).ToList(),
            };

            return View(model);
        }

        [HttpPost]  
        public async Task<IActionResult> AssignRole(RoleAssignDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return RedirectToAction(nameof(UserList));

            var currentUser = await _userManager.GetUserAsync(User);
            var currentRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles=dto.roleChecks
                .Where(x=>x.IsSelected)
                .Select(x=>x.RoleName)
                .ToList();

            var addToRole = selectedRoles.Except(currentRoles).ToList();
            var addToRemove=currentRoles.Except(selectedRoles).ToList();    


            if(addToRole.Any()) 
                await _userManager.AddToRolesAsync(user, addToRole);

            if (addToRemove.Any())
                await _userManager.RemoveFromRolesAsync(user, addToRemove);


            return RedirectToAction(nameof(UserList));

        }
    }
}