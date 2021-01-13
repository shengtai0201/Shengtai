using System;
using System.ComponentModel.DataAnnotations;

namespace Shengtai.Web.DataAnnotations
{
    public class CheckBoxAttribute : ValidationAttribute
    {
        private readonly bool isChecked;
        private readonly string errorMessage;

        public CheckBoxAttribute(bool isChecked, string errorMessage)
        {
            this.isChecked = isChecked;
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool check = Convert.ToBoolean(value);
            if (check == this.isChecked)
                return ValidationResult.Success;
            else
                return new ValidationResult(errorMessage);
            //return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}