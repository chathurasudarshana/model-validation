namespace model_validation.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class DateIsInPastAttribute: RequiredAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            bool valid = base.IsValid(value) && (DateTime)value < DateTime.Now;

            return valid;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = this.ErrorMessage;
            rule.ValidationType = "pastdate";
            yield return rule;
        }
    }
}