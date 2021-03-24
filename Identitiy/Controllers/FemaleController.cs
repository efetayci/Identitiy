using Identitiy.MyContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identitiy.Controllers
{
    [Authorize(Policy ="FemalePolicy")]
    public class FemaleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public FemaleController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddCalimAsync(int id)
        {
            
            var user = _userManager.Users.FirstOrDefault(x => x.Id==id);
            if((await _userManager.GetClaimsAsync(user)).Count == 0)
            {
                Claim claim = new Claim("gender", "female");
                await _userManager.AddClaimAsync(user, claim);
            }
           

            return RedirectToAction("USerList","Rol");
        }
    }
}
