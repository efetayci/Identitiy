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

               var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,true);
                //remember me zaten cookie de tutim mi 4.değer de lock out.Access  failed count?

                //result.Succeeded giriş başarılı mı?
                //result.IsLockedOut kilitlimi bu kişi?
                //result.IsNotAllowed aktif üye için email doğrulaması var mı?

                if (result.IsLockedOut)
                {
                    //locklama süresini gösterme
                    var gelen = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.UserName));
                    var kisitlananSure = gelen.Value;
                    var kalanDakika = kisitlananSure.Minute - DateTime.Now.Minute;
                    //
                    ModelState.AddModelError("", $"Şifreyi 5 kere yanlış girdiğiniz için {kalanDakika} dakika kitlendi");
                    return View("Inedx",model); //redirect dersek hata görünmez.
                }
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Panel");
                   
                }
                var yanlisGirilmeSayisi = await _userManager.GetAccessFailedCountAsync(await _userManager
                    .FindByNameAsync(model.UserName));
                ModelState.AddModelError("", $"Kullanici adi veya şifre hatalı{3-yanlisGirilmeSayisi} kadar yanlış girerseniz hesap bloklanır");
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
