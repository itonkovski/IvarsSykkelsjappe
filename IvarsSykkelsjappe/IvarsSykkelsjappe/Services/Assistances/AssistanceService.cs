using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(AssistanceFormModel assistance)
        {
            var assistanceData = new Assistance
            {
                Name = assistance.Name,
                Description = assistance.Description,
                Price = assistance.Price
            };

            this.dbContext.Assistances.Add(assistanceData);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<AssistanceViewModel> GetAll()
        {
            var assistances = this.dbContext
                .Assistances
                .Select(x => new AssistanceViewModel
                {
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
