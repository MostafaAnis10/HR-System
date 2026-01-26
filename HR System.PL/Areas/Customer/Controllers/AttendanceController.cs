using HR_System.BLL.ModelVM.Attendance;
using HR_System.BLL.Service.Abstraction;
using HR_System.BLL.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PL.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;
        private readonly IEmployeeService employeeService;

        public AttendanceController(IAttendanceService attendanceService, IEmployeeService employeeService)
        {
            this.attendanceService = attendanceService;
            this.employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var result = attendanceService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = employeeService.GetActiveEmployee().result;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAttendanceVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Employees = employeeService.GetActiveEmployee().result;
                return View(model);
            }

            var result = attendanceService.Create(model);

            if (result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errorMassage);
                ViewBag.Employees = employeeService.GetActiveEmployee().result;
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CheckOut(int id)
        {
            var result = attendanceService.CheckOut(id);

            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errorMassage;

            return RedirectToAction(nameof(Index));
        }


        public ActionResult Details(int id)
        {
            var result = attendanceService.GetByID(id);
            if (result.IsHaveErrorOrNo)
                return NotFound(result.errorMassage);

            return View(result.result);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = attendanceService.Delete(id);

            if (result.IsHaveErrorOrNo)
            {
                return Json(new
                {
                    success = false,
                    message = result.errorMassage
                });
            }

            return Json(new
            {
                success = true,
                message = "Attendance deleted successfully"
            });
        }

    }
}
