using HR_System.BLL.ModelVM.LeaveRequest;
using HR_System.BLL.Service.Abstraction;
using HR_System.BLL.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PL.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService leaveRequestService;
        private readonly IEmployeeService employeeService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService , IEmployeeService employeeService)
        {
            this.leaveRequestService = leaveRequestService;
            this.employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var result = leaveRequestService.GetAll();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Employees = employeeService.GetActiveEmployee().result;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateLeaveRequestVM model)
        {
            
            if (model.FromDate.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("FromDate", "An old date cannot be chosen.");
            }

            
            if (model.ToDate < model.FromDate)
            {
                ModelState.AddModelError("ToDate", "The end date must be after the start date.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Employees = employeeService.GetActiveEmployee().result;

                  return View(model);
            }

            var result = leaveRequestService.Create(model);
            if (result.IsHaveErrorOrNo)
            {
                ModelState.AddModelError("", result.errorMassage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var response = leaveRequestService.GetById(id);

            
            if (response.IsHaveErrorOrNo || response.result == null)
            {
                
                return RedirectToAction(nameof(Index));
            }

            
            return View(response.result);
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, LeaveStatus status)
        {
            var result = leaveRequestService.ChangeStatus(id, status.ToString() );

            if (result.IsHaveErrorOrNo)
                TempData["Error"] = result.errorMassage;

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = leaveRequestService.Delete(id);

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
