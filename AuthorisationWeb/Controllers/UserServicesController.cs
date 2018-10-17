using System.Buffers.Text;
using Microsoft.AspNetCore.Mvc;
using AuthorisationWeb.Models;

namespace AuthorisationWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServicesController : Controller
    {
        public Service _userServices;
        private readonly UserServicesContext _context;

        public UserServicesController(UserServicesContext context)
        {
            _context = context;
            _userServices = new ProtectionProxy(_context);
        }

        [HttpPost("signup")]
        public IActionResult Register([FromBody] User user)
        {
            if (user != null)
            {
                
                return new ObjectResult(_userServices.RegisterNewUser(user));
                
            }
            
            return BadRequest();
        }
        
        [HttpPost("signin")]
        public IActionResult LogIn([FromBody] User user)
        {
            if (user != null)
            {
               return _userServices.UserLogin(user);
            }
            
            return BadRequest();
        }
        
        [HttpGet("friends/{token}")]
        public IActionResult GetFriendsList([FromRoute] string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return new ObjectResult(_userServices.ShowFriends(token));
            }
            
            return BadRequest();
        }
        
        [HttpGet("messages/{token}")]
        public IActionResult GetMessages([FromRoute] string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return new ObjectResult(_userServices.ShowMessages(token));
            }
            
            return BadRequest();
        }
        
        
    }
}