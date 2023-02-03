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
