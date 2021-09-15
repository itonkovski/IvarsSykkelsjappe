using System;
using System.Collections.Generic;
using IvarsSykkelsjappe.Data.Models;

namespace IvarsSykkelsjappe.Models.Bookings
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime TimeSlot { get; set; }

        public string Details { get; set; }

        public string UserId { get; set; }

        public string MechanicId { get; set; }

        public string MechanicName { get; set; }

        public bool IsTaken { get; set; }

        public int AssistanceId { get; set; }

        public IEnumerable<OrderAssistanceViewModel> Assistances { get; set; }

        public string MechanicDetails { get; set; }
    }
}
