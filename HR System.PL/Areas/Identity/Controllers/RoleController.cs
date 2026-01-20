using AutoMapper;
using HR_System.BLL.ModelVM.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HR_System.PL.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleVM roleVM)
        {
            
                var getRoleByName = await roleManager.FindByNameAsync(roleVM.Name);
                if(getRoleByName is not { })
                {
                    var role = new IdentityRole() { Name = roleVM.Name};
                    var result = await roleManager.CreateAsync(role);

                return RedirectToAction("Index", "Home", new { area = "Customer" });
                }
            
          
            return View(roleVM);
        }
    }
}
