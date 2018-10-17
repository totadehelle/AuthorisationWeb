using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq.Expressions;
using System.Linq;

namespace AuthorisationWeb.Models
{
    public class ProtectionProxy : Service
    {
        private readonly Service _realService;
        private readonly UserServicesContext _context;

        public ProtectionProxy(UserServicesContext context)
        {
            _realService = new UserServices();
            _context = context;
        }

        
        public override string RegisterNewUser(User user)
        {
            var oldUser = _context.Users.FirstOrDefault(t => t.login == user.login);
            if (oldUser == null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return "New user was successfully registered!";
            }

            return "User with this login already exists!";
        }
        
        
        public override IActionResult UserLogin([FromBody] User user)
        {
            
            var targetUser = _context.Users.FirstOrDefault(t => t.login == user.login);
            
            if (targetUser == null)
            {
                return new NotFoundResult();
            }

            if (targetUser.password != user.password)
            {
                return new UnauthorizedResult();
            }
            
            string LoginAndGuid = user.login + new Guid();
            var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(LoginAndGuid));
            _context.Entry(targetUser).CurrentValues.SetValues(new Dictionary<string, object> {{"token", token}});
            _context.SaveChanges();
            return new ObjectResult(token);
        }
        

        public override string ShowFriends(string token)
        {
            var targetUser = _context.Users.FirstOrDefault(t => t.token == token);
            if (targetUser != null)
            {
                return _realService.ShowFriends(token);
            }

            return "Token is invalid!";
        }
        

        public override string ShowMessages(string token)
        {
            var targetUser = _context.Users.FirstOrDefault(t => t.token == token);
            if (targetUser != null)
            {
                return _realService.ShowMessages(token);
            }
            return "Token is invalid!";
        }
    }
}