using SchoolRegistrationForm.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegistrationForm.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Institution { get; set; }
        public string? EducationLevel { get; set; }
        public string? Gender { get; set; }
        public string? ParticipateTitle { get; set; }


        public int? OrderId { get; set; }
        public int? TransactionId { get; set; }
        public string? Status { get; set; }
    }
}
