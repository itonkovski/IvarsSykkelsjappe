namespace IvarsSykkelsjappe.Models.Bookings
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string TimeSlot { get; set; }

        public string Details { get; set; }

        public string UserId { get; set; }

        public string MechanicId { get; set; }

        public bool IsTaken { get; set; }
    }
}
