using Identitiy.Models;
using Identitiy.MyContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RolController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
        public IActionResult AddRole()
        {
            return View(new RoleViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddRoleAsync(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole
                {
                    Name = model.Name
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult UserList()
        {
            return View(_userManager.Users.ToList());
        }
        public async Task<IActionResult> AssignRoleAsync(int id)
        {
            var user =_userManager.Users.FirstOrDefault(x => x.Id == id);
            TempData["UserId"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles =  await _userManager.GetRolesAsync(user);
            List<RoleAssignViewModel> models = new List<RoleAssignViewModel>();
            foreach (var item in roles)
            {
                RoleAssignViewModel model = new RoleAssignViewModel();
                model.RoleId = item.Id;
                model.Name = item.Name;
                model.Exists = userRoles.Contains(item.Name);
                models.Add(model);
            }
            return View(models);

        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleAsync(List<RoleAssignViewModel> models)
        {
            var userId = (int)(TempData["UserId"]);
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            foreach (var item in models)
            {
                if (item.Exists)
                {
                   await _userManager.AddToRoleAsync(user, item.Name);

                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }
            return RedirectToAction("UserList");
        }

    }
}
