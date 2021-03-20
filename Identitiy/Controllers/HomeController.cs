using Identitiy.Models;
using Identitiy.MyContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(new UserSıgnInViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(UserSıgnInViewModel model)
        {
            if (ModelState.IsValid)
            {
               var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                //result.Succeeded giriş başarılı mı?
                //result.IsLockedOut kilitlimi bu kişi?
                //result.IsNotAllowed aktif üye için email doğrulaması var mı?
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                ModelState.AddModelError("", "Kullanici adi veya şifre hatalı");
            }
            return View("Index",model);
        }
        public IActionResult KayitOl()
        {
            return View(new UserSignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    UserName = model.UserName

                };
                    
              var result =  await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
