using System.ComponentModel.DataAnnotations;

namespace Calculation.Core
{
    public class InputStringValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int result;
            var isInt = int.TryParse(value.ToString(), out result);
            return isInt ? null : new ValidationResult(string.Format("Значение должно быть не более {0}", int.MaxValue));
        }
    }
}