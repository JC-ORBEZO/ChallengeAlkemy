using System;

namespace ChallengeAlkemy.ViewModels.Auth.Login
{
    public class LoginResponseViewModel
    {
        public string Token {get;set;}
        public DateTime ValidTo { get;set;}
    }
}
