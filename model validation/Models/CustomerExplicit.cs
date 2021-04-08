namespace model_validation.Models
{
    using System;

    public class CustomerExplicit
    {
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public bool TermsAccepted { get; set; }
    }
}