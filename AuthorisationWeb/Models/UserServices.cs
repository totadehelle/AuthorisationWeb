using Microsoft.AspNetCore.Mvc;

namespace AuthorisationWeb.Models
{
    public class UserServices : Service
    {
        public override string RegisterNewUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public override IActionResult UserLogin(User user)
        {
            throw new System.NotImplementedException();
        }

        public override string ShowFriends(string token)
        {
            return "Here are your friends list!";
        }

        public override string ShowMessages(string token)
        {
            return "Here are your messages!";
        }
    }
}