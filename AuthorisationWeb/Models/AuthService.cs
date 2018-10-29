using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationWeb.Models
{
    public class AuthService : IAuthService
    {
        private readonly AuthServiceContext _context;

        public AuthService(AuthServiceContext context)
        {
            _context = context;
        }

        
        public string RegisterNewUser(User user)
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
        
        
        public Task<User> UserLogin(User user)
        {
            
            var targetUser = _context.Users.FirstOrDefault(t => t.login == user.login);
            
            if (targetUser != null)
            {
                if (targetUser.password == user.password)
                {
                    string LoginAndGuid = user.login + new Guid();
                    var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(LoginAndGuid));
                    _context.Entry(targetUser).CurrentValues.SetValues(new Dictionary<string, object> {{"token", token}});
                    _context.SaveChanges();
                    return Task.FromResult(targetUser);
                }
            }

            return null;
        }
    }
}