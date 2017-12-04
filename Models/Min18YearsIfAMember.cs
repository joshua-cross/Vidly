using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            } else
            {
                if (customer.DOB == null)
                {
                    return new ValidationResult("Birthdate is required.");
                }
                else
                {

                    //if the user has given an age we need to calculate there age.
                    var age = DateTime.Today.Year - customer.DOB.Value.Year;

                    if (age >= 18)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Customer must be over the age of 18 to go on a membership.");
                    }
                }

            }
        }
    }
}