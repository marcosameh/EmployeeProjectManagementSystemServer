using System.ComponentModel.DataAnnotations;

namespace Main.BL.CustomValidators
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateGreaterThanAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateTime)value;
            var property = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            var startDate = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (endDate <= startDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
