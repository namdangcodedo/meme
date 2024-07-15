using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void AddBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.Add(bookingDetail);

        public List<BookingDetail> GetAll() => BookingDetailDAO.Instance.GetBookingDetailsList().ToList();

        public BookingDetail GetByBookingIdAndRoomID(int bookingID, int roomID) => BookingDetailDAO.Instance.GetBookingDetailsByBookingReservationIDAndRoomID(bookingID, roomID);

        public void RemoveBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.Delete(bookingDetail);
        public void UpdateBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.Instance.Update(bookingDetail);

        public List<BookingDetail> GetByBookingID(int id)
        {
            List<BookingDetail> details = GetAll();
            return details.Where(bd => bd.BookingReservationID == id).ToList();
        }
    }
}
