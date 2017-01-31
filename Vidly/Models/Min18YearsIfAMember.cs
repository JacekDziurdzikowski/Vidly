using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new  ValidationResult("Potrzebna jest data urodzenia");

            var age = DateTime.Today.Year - DateTime.Parse(customer.BirthDate).Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Użytkownik prenumeraty powinien meic wiecej niz 18 lat");


        }
    }
}