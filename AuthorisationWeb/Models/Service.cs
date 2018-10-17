using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AuthorisationWeb.Models
{
    public abstract class Service
    {
        /*public Service()
        {
            
        }
        
        public Service(DbContext context)
        {
            
        }*/
        
        public abstract string RegisterNewUser(User user);
        public abstract IActionResult UserLogin(User user);
        public abstract string ShowFriends(string token);
        public abstract string ShowMessages(string token);
    }
}