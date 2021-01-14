using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcBookingModel
    {
        public int BookingId { get; set; }
        public string BookingDate { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<int> StatusOfBooking { get; set; }

        public virtual mvcRoomModel Room { get; set; }
        public virtual mvcStatusOfBookingModel StatusOfBooking1 { get; set; }
    }
}