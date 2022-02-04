using System.ComponentModel.DataAnnotations;

namespace ChallengeAlkemy.ViewModels.Auth
{
    public class RegisterRequestModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
