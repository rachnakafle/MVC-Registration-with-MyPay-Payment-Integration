using SchoolRegistrationForm.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegistrationForm.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Participant's Name")]
        [Required(ErrorMessage = "Participant's Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Participant's Address")]
        [Required(ErrorMessage = "Participant's Address is required.")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Email Address is required."), MinLength(1), DataType(DataType.EmailAddress), EmailAddress, MaxLength(50), Display(Name = "Email")]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [Required(ErrorMessage = "Email Address is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        public string Institution { get; set; }
        public string EducationLevel { get; set; }
        public string Gender { get; set; }
        public string ParticipateTitle { get; set; }
        public string? OrderId { get; set; }
        public string? TransactionId { get; set; }
        public string? Status { get; set; }
    }
}
