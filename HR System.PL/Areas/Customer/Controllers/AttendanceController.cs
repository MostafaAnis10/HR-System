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

            
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateAttendanceVM model)
        {
            if (ModelState.IsValid)
            {
                var result = attendanceService.Create(model);
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
