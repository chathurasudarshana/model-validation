namespace model_validation.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ExcludeCharAttribute: ValidationAttribute, IClientValidatable
    {
        //https://www.c-sharpcorner.com/UploadFile/abhikumarvatsa/enabling-client-side-validation-on-custom-data-annotations-w/
        private readonly string chars;

        public ExcludeCharAttribute(string chars)
            : base("{0} contains invalid character.")
        {
            this.chars = chars;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            if (value != null)
            {
                foreach (char charValue in chars)
                {
                    string valueAsString = value.ToString();
                    if (valueAsString.Contains(charValue.ToString()))
                    {
                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                        validationResult =  new ValidationResult(errorMessage);
                        break;
                    }
                }
            }
            return validationResult;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("chars", chars);
            rule.ValidationType = "exclude";
            yield return rule;
        }
    }
}