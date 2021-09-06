using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Data.Models
{
    using static DataConstants.Category;

    public class AssistanceCategory
    {
        public AssistanceCategory()
        {
            this.Assistances = new HashSet<Assistance>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Assistance> Assistances { get; set; }
    }
}