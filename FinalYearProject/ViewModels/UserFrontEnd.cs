using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.ViewModels
{
    public class UserFrontEnd
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string Email { get; set; }
        // public string Password { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
