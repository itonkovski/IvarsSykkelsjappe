using IvarsSykkelsjappe.Models.Assistances;
using IvarsSykkelsjappe.Services.Assistances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Admin.Controllers
{
    using static WebConstants;

    public class AssistancesController : AdminController
    {
        private readonly IAssistanceService assistanceService;

        public AssistancesController(IAssistanceService assistanceService)
        {
            this.assistanceService = assistanceService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AssistanceFormModel assistanceForm)
        {
            if (!ModelState.IsValid)
            {
                return View(assistanceForm);
            }

            this.assistanceService.Add(assistanceForm);
            TempData[GlobalMessageKey] = "The service was added successfully.";
            return RedirectToAction(nameof(AllAssistances));
        }

        public IActionResult AllAssistances()
        {
            var assistances = this.assistanceService.GetAll();
            return View(assistances);
        }

        public IActionResult Delete(int id)
        {
            this.assistanceService.Delete(id);
            TempData[GlobalMessageKey] = "The service was deleted successfully.";
            return RedirectToAction(nameof(AllAssistances));
        }
    }
}
