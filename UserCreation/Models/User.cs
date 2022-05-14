using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserCreation.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "First Name is required")]
        [StringLength(50,MinimumLength =2,ErrorMessage = "First Name has to be at least of length 2 and at most of length 50")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name has to be at least of length 2 and at most of length 50")]
        public string lastName { get; set; }

        [Required]
        [GenderRange(ErrorMessage ="Gender can be only Male or Female.")]
        public string gender { get; set; }

        [Required(ErrorMessage ="Date of Birth is necessary.")]
        public string dob { get; set; }

        public string city { get; set; }

        [Required(ErrorMessage = "Phone no. is required.")]
        [RegularExpression(@"^(?:\+88|88)?(01[3-9]\d{8})$",ErrorMessage ="Provide a valid phone no.")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]  
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        public string email { get; set; }
    }

    public class GenderRangeAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "Male" || value.ToString() == "Female")
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
    }
}
