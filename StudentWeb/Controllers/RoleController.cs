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
    [Authorize]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager { get; }
        UserManager<AppUser> _userManager { get; }

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                await _roleManager.CreateAsync(new IdentityRole(name));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string name)
        {
            if (string.IsNullOrEmpty(name))
                return View(null);
            var role = await _roleManager.FindByNameAsync(name);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string name)
        {
            var result = await _roleManager.FindByNameAsync(name);
            if (result != null)
                await _roleManager.DeleteAsync(result);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AssignRole() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(AssignRoleModel ass)
        {
            if(ModelState.IsValid)
            {
                var userTask = _userManager.FindByNameAsync(ass.User);
                var roleTask = _roleManager.FindByNameAsync(ass.Role);
                var user = await userTask;
                var role = await roleTask;
                if(user != null && role != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                }
            }
            return View();
        }
    }
}