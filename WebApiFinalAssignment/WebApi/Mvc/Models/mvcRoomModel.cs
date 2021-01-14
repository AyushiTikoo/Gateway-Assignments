using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcRoomModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcRoomModel()
        {
            this.Bookings = new HashSet<mvcBookingModel>();
        }

        public int RoomId { get; set; }
        public int HotelId { get; set; }
        [DisplayName("Room Name")]
        [Required(ErrorMessage = "Room Name is required")]
        public string RoomName { get; set; }

        [DisplayName("Room Category")]
        [Required(ErrorMessage = "Room Category is required")]
        public int RoomCategory { get; set; }
        [DisplayName("Room Price")]
        [Required(ErrorMessage = "Room Price is required")]
        public string RoomPrice { get; set; }
        [DisplayName("Is Available")]
        [Required(ErrorMessage = "Mention Availability")]
        public string IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcBookingModel> Bookings { get; set; }
        public virtual mvcHotelModel Hotel { get; set; }
        public virtual mvcRoomCategoryModel RoomCategory1 { get; set; }
    }
}