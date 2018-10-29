using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthorizationWeb.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;

namespace AuthorizationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(AuthServiceContext context, IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public IActionResult Register([FromBody] User user)
        {
            if (user != null)
            {
                return new ObjectResult(_authService.RegisterNewUser(user));
            }
            
            return BadRequest();
        }
        
        [HttpPost("signin")]
        public async Task<IActionResult> LogIn([FromBody] User user)
        {
            if (user != null)    
            {
                
                var receivedUser = await _authService.UserLogin(user);
                if (receivedUser != null)
                {
                    
                    return await SignInUser(receivedUser);
                }
                
                return new UnauthorizedResult();
            }

            else
            {
                return BadRequest();
            }
        }
        
        private async Task<IActionResult> SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.login),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return new AcceptedResult();
        }
    }
    
    
    
}