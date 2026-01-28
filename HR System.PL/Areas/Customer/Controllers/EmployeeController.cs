using HR_System.BLL.Helper;
using HR_System.BLL.ModelVM.Department;
using HR_System.BLL.ModelVM.Employee;
using HR_System.BLL.Service.Abstraction;
using HR_System.BLL.Service.Implementation;
using HR_System.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HR_System.PL.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;

        List<Department>DepartmentsName;
        public EmployeeController(IEmployeeService employeeService ,IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;

            
        }
        public IActionResult Index()
        {
            var result = employeeService.GetActiveEmployee();

            
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //var Department = departmentService.GetDepartment();
            //ViewBag.Department = Department;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (string.IsNullOrWhiteSpace(model.Name) || model.Salary <= 0)
            {
                ModelState.AddModelError("", "Invalid employee data");
                return View(model);
            }

            // رفع الصورة لو موجودة
            string? fileName = null;
            if (model.File != null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                fileName = Upload.UploadFile("Files", model.File);
            }

            // تمرير اسم الملف للـ Service مباشرة
            var result = employeeService.Create(model, fileName);

            if (!result.IsHaveErrorOrNo)
                return RedirectToAction("Index");

            ViewBag.Error = result.errorMassage;
            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var empById = employeeService.GetByID(id);
            if (empById.IsHaveErrorOrNo)
                return NotFound(empById.errorMassage);


            return View(empById.result);
        }

        [HttpPost]
        public IActionResult Edit(EditEmployeeVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // الاحتفاظ باسم الصورة القديمة قبل أي تعديل لحذفها لاحقاً
            string? oldFileName = model.File;
            bool isNewFileUploaded = false;

            if (model.NewFile != null)
            {
                // 1. رفع الصورة الجديدة وتحديث اسم الملف في الموديل
                model.File = Upload.UploadFile("Files", model.NewFile);
                isNewFileUploaded = true;
            }

            // 2. إرسال الموديل للـ Service (سواء بالاسم القديم أو الجديد)
            var result = employeeService.Edit(model);

            if (!result.IsHaveErrorOrNo) // في حالة النجاح
            {
                // 3. حذف الصورة القديمة من السيرفر "فقط" إذا رفعنا واحدة جديدة وكانت هناك صورة أصلاً
                if (isNewFileUploaded && !string.IsNullOrEmpty(oldFileName))
                {
                    string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", oldFileName);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                return RedirectToAction("Index");
            }

            // 4. في حالة الفشل: نحذف الصورة "الجديدة" التي رُفعت لأن العملية لم تكتمل في الداتابيز
            if (isNewFileUploaded)
            {
                string newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", model.File!);
                if (System.IO.File.Exists(newPath)) System.IO.File.Delete(newPath);
            }

            ModelState.AddModelError("", result.errorMassage);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var result = employeeService.GetDetails(id);
            if (result.IsHaveErrorOrNo)
                return NotFound(result.errorMassage);

            return View(result.result);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = employeeService.Delete(id);
            if (result.IsHaveErrorOrNo)
                return Json(new { success = false, message = result.errorMassage });

            return Json(new { success = true, message = "Employee deleted successfully" });
        }


    }
}
