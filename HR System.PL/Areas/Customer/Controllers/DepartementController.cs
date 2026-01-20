using HR_System.BLL.ModelVM.Department;
using HR_System.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PL.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class DepartementController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartementController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var result = departmentService.GetDepartment();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartementVM model)
        {
            if (ModelState.IsValid)
            {
                var result = departmentService.CreateDepartment(model);

                if(!result.IsHaveErrorOrNo)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.errorMassage);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = departmentService.GetByID(id);

            if (result == null || result.IsHaveErrorOrNo || result.result == null)
            {
                return NotFound(result?.errorMassage ?? "Department not found");
            }

            return View(result.result);
        }
        [HttpPost]
        public IActionResult Edit(EditDepartementVM model)
        {
            if (ModelState.IsValid)
            {
                var result =departmentService.EditDepartment(model);

                if (!result.IsHaveErrorOrNo)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.errorMassage);
            }

            return View(model);
        }
    }
}
