namespace model_validation.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class MustBeTrueAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool valid = value is bool && (bool)value;

            return valid;
        }
    }
}