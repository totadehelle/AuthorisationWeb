using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuthorizationWeb.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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
        public async Task LogIn([FromBody] User user)
        {
            if (user != null)    
            {
                
                var receivedUser = await _authService.UserLogin(user);
                if (receivedUser != null)
                {
                    var identity = GetIdentity(user);
                    
                    var now = DateTime.UtcNow;
                    // создаем JWT-токен
                    var jwt = new JwtSecurityToken(
                        AuthOptions.ISSUER,
                        AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    _authService.SaveToken(receivedUser.login, encodedJwt);
             
                    var response = new
                    {
                        access_token = encodedJwt,
                        username = identity.Name
                    };
 
                    // сериализация ответа
                    Response.ContentType = "application/json";
                    await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                    
                    //return await SignInUser(receivedUser);
                }
                
                else await Response.WriteAsync("Invalid username or password.");
            }

            else
            {
                await Response.WriteAsync("Bad request.");
            }
        }
        
        
        //???
        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.login),
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
        }
        
        //???
        
        
        
        private async Task<IActionResult> SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.token),
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return new AcceptedResult();
        }
    }
    
    
    
}