using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingDetailRepository
    {
        List<BookingDetail> GetAll();
        BookingDetail GetByBookingIdAndRoomID(int bookingID, int roomID);
        void AddBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void RemoveBookingDetail(BookingDetail bookingDetail);
        List<BookingDetail> GetByBookingID(int id);
    }
}
