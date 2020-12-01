using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormValidation.Models
{
    public class MinPassLength : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string pass = Convert.ToString(value);
                char[] one = pass.ToCharArray();
                int len = pass.Length;
                if (len < 10)
                {
                    return new ValidationResult("Minimum length must be " + 10);
                }             
                if (!pass.Any(char.IsDigit))
                {
                    return new ValidationResult("Password must contain atleast one digit");
                }
                if (!pass.Any(ch => !Char.IsLetterOrDigit(ch)))
                {
                    return new ValidationResult("Password must contain atleast one special character");
                }
            }
            return ValidationResult.Success;
        }
    }
}