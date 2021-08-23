using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IvarsSykkelsjappe.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }
    }
}
