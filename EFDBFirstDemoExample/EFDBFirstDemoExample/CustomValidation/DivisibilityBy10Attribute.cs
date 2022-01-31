using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFDBFirstDemoExample.CustomValidation
{
    public class DivisibilityBy10Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double Price = Convert.ToDouble(value);
            if (Price % 10 == 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(this.ErrorMessage);
            }
            //return base.IsValid(value, validationContext);
        }
    }
}