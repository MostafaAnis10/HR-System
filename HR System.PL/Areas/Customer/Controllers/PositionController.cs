using HR_System.BLL.ModelVM.Position;
using HR_System.BLL.Service.Abstraction;
using HR_System.BLL.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.PL.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class PositionController : Controller
    {
        private readonly IPositionService positionService;

        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        public IActionResult Index()
        {
            var result = positionService.GetPosition();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatePositionVM model)
        {
            if (ModelState.IsValid)
            {
                var result = positionService.CreatePosition(model);
                if (!result.IsHaveErrorOrNo)
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
            var result = positionService.GetByID(id);
            if (result == null || result.IsHaveErrorOrNo || result.result == null)
            {
                return NotFound(result?.errorMassage ?? "Department not found");
            }

            return View(result.result);
            
        }
        [HttpPost]
        public IActionResult Edit(EditPositionVM model)
        {
            if (ModelState.IsValid)
            {
                var result = positionService.EditPosition(model);
                if (!result.IsHaveErrorOrNo)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.errorMassage);
            }
            return View(model);

        }

        [HttpPost]
        public IActionResult Delete(int id) // حذفنا [FromBody]
        {
            var result = positionService.Delete(id);
            if (result.IsHaveErrorOrNo)
                return Json(new { success = false, message = result.errorMassage });

            return Json(new { success = true, message = "Department deleted successfully" });
        }
    }
}
