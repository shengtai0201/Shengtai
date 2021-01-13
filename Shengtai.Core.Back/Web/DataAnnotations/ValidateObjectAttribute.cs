﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shengtai.Web.DataAnnotations
{
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = new ValidationContext(value, null, null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(value, context, results, true);
            if (results.Count != 0)
            {
                var compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!",
                    validationContext.DisplayName));
                results.ForEach(compositeResults.AddResult);

                return compositeResults;
            }

            return ValidationResult.Success;
        }
    }
}