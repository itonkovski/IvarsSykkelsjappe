using System.Threading.Tasks;
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
        public async Task<IActionResult> Create(AssistanceFormModel assistanceForm)
        {
            if (!ModelState.IsValid)
            {
                return View(assistanceForm);
            }

            await this.assistanceService.AddAsync(assistanceForm);
            TempData[GlobalMessageKey] = "The service was added successfully.";
            return RedirectToAction(nameof(AllAssistances));
        }

        public IActionResult AllAssistances()
        {
            var assistances = this.assistanceService.GetAll();
            return View(assistances);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.assistanceService.DeleteAsync(id);
            TempData[GlobalMessageKey] = "The service was deleted successfully.";
            return RedirectToAction(nameof(AllAssistances));
        }
    }
}
