using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ViewBookingForAdmin
    {
        public int bookingReservationID { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string roomNumber { get; set; }
        public string roomDetails { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
    }
}
