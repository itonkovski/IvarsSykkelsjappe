using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Data;
using IvarsSykkelsjappe.Data.Models;
using IvarsSykkelsjappe.Models.Bookings;

namespace IvarsSykkelsjappe.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext dbContext;

        public BookingService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(BookingFormModel booking, string userId)
        {

            var bookingData = new Booking
            {
                FullName = booking.FullName,
                Email = booking.Email,
                PhoneNumber = booking.PhoneNumber,
                TimeSlot = booking.TimeSlot,
                Details = booking.Details,
                UserId = userId,
            };
            await this.dbContext.Bookings.AddAsync(bookingData);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<BookingViewModel> GetAllBookings()
        {
                var bookings = this.dbContext
                .Bookings
                .Select(x => new BookingViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    TimeSlot = x.TimeSlot.ToString("yyyy-MM-dd HH:mm"),
                    Details = x.Details,
                    UserId = x.UserId,
                    MechanicId = x.MechanicId,
                    MechanicName = x.MechanicName
                })
                .OrderByDescending(x => x.Id)
                .ToList();

            return bookings;
        }

        public async Task DeleteAsync(int id)
        {
            var booking = this.dbContext.Bookings.Find(id);
            this.dbContext.Bookings.Remove(booking);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<BookingViewModel> MyBookings(string userId)
        {
            var bookings = this.dbContext
                .Bookings
                .Where(x => x.UserId == userId)
                .Select(x => new BookingViewModel
                {
                    Id = x.Id,
                    TimeSlot = x.TimeSlot.ToString("yyyy-MM-dd HH:mm"),
                    Details = x.Details,
                    
                })
                .OrderByDescending(x => x.Id)
                .ToList();

            return bookings;
        }

        public async Task TakeMechanicAsync(int id, string mechanicId, string mechanicName)
        {
            var orderData = this.dbContext
                .Bookings
                .FirstOrDefault(x => x.Id == id);

            orderData.MechanicId = mechanicId;
            orderData.MechanicName = mechanicName;
            orderData.IsTaken = true;
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<BookingViewModel> MyOrders(string mechanicId)
        {
            var orders = this.dbContext
                .Bookings
                .Where(x => x.MechanicId == mechanicId)
                .Select(x => new BookingViewModel
                {
                    Id = x.Id,
                    TimeSlot = x.TimeSlot.ToString("yyyy-MM-dd HH:mm"),
                    Details = x.Details,

                })
                .OrderByDescending(x => x.Id)
                .ToList();

            return orders;
        }

        public IEnumerable<BookingViewModel> GetAllBookingsForToday()
        {
            var currentDate = DateTime.UtcNow.Date;

            var bookings = this.dbContext
                .Bookings
                .Where(x => x.IsTaken == false && x.TimeSlot.Date == currentDate)
                .Select(x => new BookingViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    TimeSlot = x.TimeSlot.ToString("yyyy-MM-dd HH:mm"),
                    Details = x.Details,
                    UserId = x.UserId,
                    MechanicId = x.MechanicId
                    
                })
                .OrderByDescending(x => x.Id)
                .ToList();

            return bookings;
        }

        public IEnumerable<OrderAssistanceViewModel> GetAssistances()
         => this.dbContext
            .Assistances
            .Select(x => new OrderAssistanceViewModel
            {
                AssistanceId = x.Id,
                Name = x.Name,
                Price = x.Price
            })
            .ToList();

        public OrderDetailsViewModel GetByMechanic(int id)
        {
            var order = this.dbContext
                .Bookings
                .Where(x => x.Id == id)
                .Select(x => new OrderDetailsViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    TimeSlot = x.TimeSlot,
                    Details = x.Details
                })
                .FirstOrDefault();
            return order;
        }
    }
}
