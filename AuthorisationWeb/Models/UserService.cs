using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationWeb.Models
{
    
    
    //mock service
    public class UserService : IUserService
    {
        public string ShowFriends()
        {
            return "Here are your friends list!";
        }

        public string ShowMessages()
        {
            return "Here are your messages!";
        }
    }
}