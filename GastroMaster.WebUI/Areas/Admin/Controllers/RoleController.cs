using ClosedXML.Excel;
using GastroMaster.DtoLayer.DTOS.AppRoleDtos;
using GastroMaster.DtoLayer.DTOS.UserDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace GastroMaster.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
                RoleId = x.Id,
                RoleName = x.Name,
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
        public async Task<IActionResult> ExportUsersToPdf()
        {
            var users = await _userManager.Users.OrderBy(x => x.UserName).ToListAsync();
            var values = new List<UserWithRolesDto>();
            foreach (var item in users)
            {
                values.Add(new UserWithRolesDto
                {
                    UserId = item.Id,
                    NameSurname = item.Name + " " + item.Surname,
                    UserName = item.UserName,
                    Email = item.Email,
                    Roles = (await _userManager.GetRolesAsync(item)).ToList()
                });
            }

            var adminCount = values.Count(x => x.Roles.Contains("Admin"));

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(column =>
                    {
                        column.Item().Text("GastroMaster").FontSize(20).Bold().FontColor("#1572e8");
                        column.Item().Text("Kullanıcı Raporu").FontSize(14).SemiBold();
                        column.Item().Text($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm} · Toplam {values.Count} kullanıcı · {adminCount} admin").FontColor(Colors.Grey.Darken1);
                        column.Item().PaddingVertical(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background("#1572e8").Padding(6).Text("#").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Ad Soyad").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Kullanıcı Adı").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("E-posta").FontColor(Colors.White).Bold();
                            header.Cell().Background("#1572e8").Padding(6).Text("Roller").FontColor(Colors.White).Bold();
                        });

                        int count = 0;
                        foreach (var item in values)
                        {
                            count++;
                            var background = count % 2 == 0 ? Colors.Grey.Lighten4 : Colors.White;
                            var isAdmin = item.Roles.Contains("Admin");

                            table.Cell().Background(background).Padding(6).Text(count.ToString());
                            table.Cell().Background(background).Padding(6).Text(item.NameSurname).SemiBold();
                            table.Cell().Background(background).Padding(6).Text(item.UserName);
                            table.Cell().Background(background).Padding(6).Text(item.Email).FontColor(Colors.Grey.Darken2);
                            table.Cell().Background(background).Padding(6)
                                .Text(item.Roles.Any() ? string.Join(", ", item.Roles) : "Rol yok")
                                .FontColor(isAdmin ? "#e63946" : Colors.Grey.Darken2)
                                .SemiBold();
                        }
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Sayfa ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
                });
            }).GeneratePdf();

            var fileName = $"Kullanicilar-{DateTime.Now:dd-MM-yyyy}.pdf";
            return File(pdf, "application/pdf", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var users = await _userManager.Users.OrderBy(x => x.UserName).ToListAsync();
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Kullanıcılar");

                workSheet.Cell(1, 1).Value = "#";
                workSheet.Cell(1, 2).Value = "Ad";
                workSheet.Cell(1, 3).Value = "Soyad";
                workSheet.Cell(1, 4).Value = "Kullanıcı Adı";
                workSheet.Cell(1, 5).Value = "Mail";
                workSheet.Cell(1, 6).Value = "Telefon Numarası";
                workSheet.Cell(1, 7).Value = "Roller";

                var headerRow = workSheet.Range(1, 1, 1, 7);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#e67e22");
                headerRow.Style.Font.FontColor = XLColor.White;

                int row = 2;
                foreach (var item in users)
                {
                    var roles = await _userManager.GetRolesAsync(item);

                    workSheet.Cell(row, 1).Value = row - 1;
                    workSheet.Cell(row, 2).Value = item.Name;
                    workSheet.Cell(row, 3).Value = item.Surname;
                    workSheet.Cell(row, 4).Value = item.UserName;
                    workSheet.Cell(row, 5).Value = item.Email;
                    workSheet.Cell(row, 6).Value = item.PhoneNumber;
                    workSheet.Cell(row, 7).Value = string.Join(", ", roles);
                    row++;
                }

                workSheet.Columns().AdjustToContents();
                workSheet.SheetView.FreezeRows(1);

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"Kullanicilar-{DateTime.Now:dd-MM-yyyy}.xlsx");
                }
            }
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
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null || role.Name == "Admin")
                return RedirectToAction(nameof(Index));

            await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return RedirectToAction(nameof(Index));

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
            if (role == null)
                return RedirectToAction(nameof(Index));

            if (role.Name == "Admin")
            {
                ModelState.AddModelError("", "Admin rolünün adı değiştirilemez");
                return View(dto);
            }

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
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return RedirectToAction(nameof(UserList));

            var allRoles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new RoleAssignDto
            {
                UserId = userId,
                UserName = user.UserName,
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

            var selectedRoles = dto.roleChecks
                .Where(x => x.IsSelected)
                .Select(x => x.RoleName)
                .ToList();

            if (user.Id == currentUser.Id && currentRoles.Contains("Admin") && !selectedRoles.Contains("Admin"))
                selectedRoles.Add("Admin");

            var addToRole = selectedRoles.Except(currentRoles).ToList();
            var addToRemove = currentRoles.Except(selectedRoles).ToList();

            if (addToRole.Any())
                await _userManager.AddToRolesAsync(user, addToRole);

            if (addToRemove.Any())
                await _userManager.RemoveFromRolesAsync(user, addToRemove);

            return RedirectToAction(nameof(UserList));
        }
    }
}