using System;
namespace IvarsSykkelsjappe.Models.Bookings
{
    public class OrderAssistanceViewModel
    {
        public int AssistanceId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
