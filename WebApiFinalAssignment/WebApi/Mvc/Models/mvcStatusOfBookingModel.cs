using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcStatusOfBookingModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcStatusOfBookingModel()
        {
            this.Bookings = new HashSet<mvcBookingModel>();
        }

        public int StatusOfBooking1 { get; set; }
        public string StatusOfBookingName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcBookingModel> Bookings { get; set; }
    }
}