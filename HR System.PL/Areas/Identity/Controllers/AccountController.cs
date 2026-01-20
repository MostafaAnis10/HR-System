using HR_System.BLL.ModelVM.AccountVM;
using HR_System.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;

namespace HR_System.PL.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid) 
              {
                //mapping

                ApplicationUser applicationUser = new() 

                {
                     UserName = registerVM.UserName,
                     Email = registerVM.Email,
                     EmployeeId = registerVM.EmployeeId,
                };
                //save database
                var result = await _userManager.CreateAsync(applicationUser, registerVM.Password); 

                if (result.Succeeded) 
                {
                    //cookie
                   var IsHaveRole = await _userManager.AddToRoleAsync(applicationUser, "Admin");

                    if(IsHaveRole !=null)
                    {
                        var resultRole = await _userManager.AddToRoleAsync(applicationUser, "Admin");

                    }

                    //await _signInManager.SignInAsync(applicationUser, false); 

                    return RedirectToAction("Index", "Home", new { area = "Customer" }); 
                }

                else 
                {
                     //Error
                    ModelState.AddModelError("Password", "Don't match the constraints"); 
                }

                foreach (var item in result.Errors) 
                {
                    ModelState.AddModelError("", item.Description); 
                }

            }
           return View(registerVM); 
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                //check found
                var appUser = await _userManager.FindByEmailAsync(loginVM.Email);
                if (appUser != null)
                {
                    var result = await _userManager.CheckPasswordAsync(appUser, loginVM.Password);
                    if (result)
                    {
                        //Login
                        await _signInManager.SignInAsync(appUser, loginVM.RememberMe);
                        return RedirectToAction("Index", "Home", new { area = "Customer" });
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Don't match the constraints");
                        ModelState.AddModelError("Email", "Can not found the email");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Don't match the constraints");
                    ModelState.AddModelError("Email", "Can not found the email");
                }


            }
            return View(loginVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }
    }
}
