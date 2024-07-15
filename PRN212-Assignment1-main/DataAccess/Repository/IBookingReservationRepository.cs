using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingReservationRepository
    {
        List<BookingReservation> GetAll();
        BookingReservation GetByID(int id);
        void AddBooking(BookingReservation bookingReservation);
        void RemoveBooking(BookingReservation bookingReservation);
        void UpdateBooking(BookingReservation bookingReservation);
        List<BookingReservation> GetByCustomerID(int id);
        List<BookingReservation> GetBookingByDateRange(DateTime start, DateTime end);
    }
}
