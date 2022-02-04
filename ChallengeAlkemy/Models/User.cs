using Microsoft.AspNetCore.Identity;

namespace ChallengeAlkemy.Models
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
