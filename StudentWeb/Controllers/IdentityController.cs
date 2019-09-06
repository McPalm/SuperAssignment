using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models.Identity;
using StudentWeb.Models.Identity.ViewModels;

namespace StudentWeb.Controllers
{
    public class IdentityController : Controller
    {
        UserManager<AppUser> _UserManager { get; }
        SignInManager<AppUser> _SignInManager { get; }

        public IdentityController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        public IActionResult Index() => View();

        [HttpGet]
        [Route("/Register")]
        public IActionResult Register() => View();
        [HttpPost]
        [Route("/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterAppUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _UserManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ViewData["error"] = string.Join(", and ", result.Errors.Select(err => err.Description));
            }
            return View();
        }

        [HttpGet]
        [Route("/SignIn")]
        public IActionResult SignIn() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/SignIn")]
        public async Task<IActionResult> SignIn(SignInAppUser user)
        {
            if(ModelState.IsValid)
            {
                var appUser = await _UserManager.FindByNameAsync(user.Name);
                appUser = appUser ?? new AppUser();
                var result = await _SignInManager.PasswordSignInAsync(appUser, user.Password, false, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ViewData["error"] = "Login Failed, verify username and password!";
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        [Route("/SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
    }
}