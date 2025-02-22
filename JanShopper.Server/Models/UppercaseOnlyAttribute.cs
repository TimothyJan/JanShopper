using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JanShopper.Server.Validation
{
    public class UppercaseOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success; // Let the Required attribute handle empty values
            }

            string input = value.ToString();

            // Use a regular expression to check if the string contains only uppercase letters
            if (!Regex.IsMatch(input, @"^[A-Z\s]+$"))
            {
                return new ValidationResult("The field must contain only uppercase letters.");
            }

            return ValidationResult.Success;
        }
    }
}