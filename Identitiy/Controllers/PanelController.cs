using Identitiy.Models;
using Identitiy.MyContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
     private readonly UserManager<AppUser> _userManager;
     private readonly SignInManager<AppUser> _signInManager;
        public PanelController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel
            {
                Email=user.Email,
                Name=user.Name,
                SurName=user.SurName,
                PhoneNumber=user.PhoneNumber,
                PictureUrl=user.PictureUrl,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.Picture != null)
            {
                var uygYer = Directory.GetCurrentDirectory();
                var uzanti = Path.GetExtension(model.Picture.FileName); //Name sadece adı filename uzantıyla beraber
                var resimAd = Guid.NewGuid() + uzanti;
                var kayıtYer = uygYer + "/wwwroot/img/" + resimAd;
              
                using var stream = new FileStream(kayıtYer,FileMode.Create);
                await model.Picture.CopyToAsync(stream);
                user.PictureUrl = resimAd;
            }
            user.Name = model.Name;
            user.SurName = model.SurName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            var Result = await _userManager.UpdateAsync(user);

            if (Result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in Result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
      
            return View(model);
        }
        public async Task<IActionResult> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
