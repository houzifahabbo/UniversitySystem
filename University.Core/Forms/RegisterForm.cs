using System.ComponentModel.DataAnnotations;

namespace University.Core.Forms
{
    public enum UserRole
    {
        Student,
        Teacher,
    }
    public class RegisterForm
    {
        [Required]
        public string FirstNmae { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$")]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
