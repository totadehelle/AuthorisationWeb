using Microsoft.AspNetCore.Mvc;
using AuthorizationWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationWeb.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class UserServiceController : Controller
    {
        private readonly IUserService _service;

        public UserServiceController(IUserService service)
        {
            _service = service;
        }
        
        [HttpGet("friends")]
        public IActionResult GetFriendsList()
        {
            return new ObjectResult(_service.ShowFriends());
        }
        
        [HttpGet("messages")]
        public IActionResult GetMessages()
        {
            return new ObjectResult(_service.ShowMessages());
        }
    }
}