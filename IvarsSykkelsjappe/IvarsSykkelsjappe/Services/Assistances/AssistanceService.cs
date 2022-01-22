using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Data;
using IvarsSykkelsjappe.Data.Models;
using IvarsSykkelsjappe.Models.Assistances;

namespace IvarsSykkelsjappe.Services.Assistances
{
    public class AssistanceService : IAssistanceService
    {
        private readonly ApplicationDbContext dbContext;

        public AssistanceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(AssistanceFormModel assistance)
        {
            var assistanceData = new Assistance
            {
                Name = assistance.Name,
                Description = assistance.Description,
                Price = assistance.Price
            };

            await this.dbContext.Assistances.AddAsync(assistanceData);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var assistance = this.dbContext.Assistances.Find(id);
            this.dbContext.Assistances.Remove(assistance);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<AssistanceViewModel> GetAll()
        {
            var assistances = this.dbContext
                .Assistances
                .Select(x => new AssistanceViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price
                })
                .OrderByDescending(x => x.Name)
                .ToList();

            return assistances;
        }
    }
}
