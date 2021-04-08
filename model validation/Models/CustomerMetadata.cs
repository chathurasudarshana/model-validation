namespace model_validation.Models
{
    using model_validation.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CustomerMetadata
    {
        [Required]
        [StringLength(20, MinimumLength =2)]
        [ExcludeChar("/.,!@#$%")]
        public string FirstName { get; set; }

        [ExcludeChar("/.,!@#$%", ErrorMessage = "Last Name contains invalid character./.,!@#$%")]
        [Remote("ValidateLastName","Home")]
        public string LastName { get; set; }

        [DataType(DataType.Date)] // Fill a calendar
        [DateIsInPast(ErrorMessage ="Please enter a date in past..")]
        public DateTime Birthday { get; set; }

        [MustBeTrue(ErrorMessage ="You must accept the terms")]
        public bool TermsAccepted { get; set; }
    }
}