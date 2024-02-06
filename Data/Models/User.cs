using System.ComponentModel.DataAnnotations;

namespace FribergRentals.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid e-mail address")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match")]
        [Display(Name = "Confirm password")]
        public string VerifyPassword { get; set; }

        public virtual List<Order>? Orders { get; set; }
        public string? SessionToken { get; set; }
    }
}
