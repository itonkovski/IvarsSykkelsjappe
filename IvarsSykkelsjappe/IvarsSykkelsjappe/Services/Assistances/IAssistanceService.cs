using System.Collections.Generic;
using IvarsSykkelsjappe.Models.Assistances;

namespace IvarsSykkelsjappe.Services.Assistances
{
    public interface IAssistanceService
    {
        void Add(AssistanceFormModel assistance);

        IEnumerable<AssistanceViewModel> GetAll();
    }
}
