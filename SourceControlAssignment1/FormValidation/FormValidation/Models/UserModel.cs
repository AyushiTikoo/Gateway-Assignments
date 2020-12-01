using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace FormValidation.Models
{
    [Bind(Exclude = "Id")]
    public class UserModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email ID is Required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirm Email is Required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm Email")]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "Email Not Matched")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1-120 in years.")]
        public int Age { get; set; }

        [Required(ErrorMessage ="Mobile Number is required")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage ="Not valid Format")]
        [Display(Name = "Mobile number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinPassLength]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
    }
}