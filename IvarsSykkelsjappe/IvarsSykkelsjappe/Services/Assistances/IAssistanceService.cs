using System.Collections.Generic;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Models.Assistances;

namespace IvarsSykkelsjappe.Services.Assistances
{
    public interface IAssistanceService
    {
        Task AddAsync(AssistanceFormModel assistance);

        IEnumerable<AssistanceViewModel> GetAll();

        Task DeleteAsync(int id);
    }
}
