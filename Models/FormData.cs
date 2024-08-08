using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtDocumentGenerator.Models
{
	public class FormData
	{
        public string? DocumentId { get; set; } // Unique document ID
        public string? FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string? LastName { get; set; }
		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Picture { get; set; } // Base64 string
		public string? Signature { get; set; } // Base64 string for signature or thumbprint
		public DateTime GenerationDate { get; set; } = DateTime.UtcNow; // Generation date and time
        public string? DocumentContent { get; set; }
        public string? Location { get; set; }

        public string? DocumentUrl {  get; set; }

        public DateTime DocumentLostDate { get; set; } = DateTime.Now;
        public string DocumentLostTimeString { get; set; } = DateTime.Now.ToString("HH:mm");

        public List<string> SelectedLostItems { get; set; } = new List<string>();

        [NotMapped]
        public TimeSpan DocumentLostTime
        {
            get => TimeSpan.Parse(DocumentLostTimeString);
            set => DocumentLostTimeString = value.ToString(@"hh\:mm");
        }
    }

    // Custom validation class to set start date constraint
    public class StartDateValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime startDate)
            {
                if (startDate < DateTime.Today || startDate > DateTime.Today.AddDays(30))
                {
                    return new ValidationResult("Start Date cannot be before today and must be within the next 30 days.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid Start Date");
        }
    }
}
