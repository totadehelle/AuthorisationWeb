using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AuthorizationWeb.Models
{
    public interface IAuthService
    {
        string RegisterNewUser(User user);
        Task<User> UserLogin(User user);
        void SaveToken(string login, string token);
    }
}