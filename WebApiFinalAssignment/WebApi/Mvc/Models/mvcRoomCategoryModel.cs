using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class mvcRoomCategoryModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcRoomCategoryModel()
        {
            this.Rooms = new HashSet<mvcRoomModel>();
        }

        public int RoomCategory1 { get; set; }

        [DisplayName("Room Category")]
        [Required(ErrorMessage = "Room Category is required")]
        public string RoomCategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcRoomModel> Rooms { get; set; }
    }
}