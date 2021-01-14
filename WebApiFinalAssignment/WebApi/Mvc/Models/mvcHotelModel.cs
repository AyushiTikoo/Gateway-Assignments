using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class mvcHotelModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcHotelModel()
        {
            this.Rooms = new HashSet<mvcRoomModel>();
        }

        public int HotelId { get; set; }

        [DisplayName("Hotel Name")]
        [Required(ErrorMessage = "Hotel Name is required")]
        public string HotelName { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [DisplayName("Pin Code")]
        [Required(ErrorMessage = "Pin code is required")]
        public string Pincode { get; set; }

        [DisplayName("Contact Number")]
        [Required(ErrorMessage = "ContactNumber is required")]
        public string ContactNumber { get; set; }

        [DisplayName("Contact Person")]
        [Required(ErrorMessage = "Contact Person is required")]
        public string ContactPerson { get; set; }

        [DisplayName("Website")]
        [Required(ErrorMessage = "Website is required")]
        public string Website { get; set; }

        [DisplayName("Facebook Id")]
        [Required(ErrorMessage = "Facebook Id is required")]
        public string Facebook { get; set; }

        [DisplayName("Twitter Id")]
        [Required(ErrorMessage = "Twitter Id is required")]
        public string Twitter { get; set; }

        [DisplayName("Active Status")]
        [Required(ErrorMessage = "Mention Active Status is required")]

        public string IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcRoomModel> Rooms { get; set; }
    }
}