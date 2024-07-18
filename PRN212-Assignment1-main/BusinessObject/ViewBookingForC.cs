using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ViewBookingForC
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomStatus { get; set; } // 1: Active, 2: Deleted
        public decimal RoomPricePerDate { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }

    }
}
