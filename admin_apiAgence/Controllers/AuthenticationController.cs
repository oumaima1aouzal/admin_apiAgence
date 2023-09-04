using admin_apiAgence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace admin_apiAgence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationServiceManager _JwtAuthenticationServiceManager;
        private readonly IConfiguration _config;
        private readonly UserContext _context;
        public AuthenticationController(IJwtAuthenticationServiceManager jwtAuthenticationServiceManager, IConfiguration config, UserContext _contex)
        {
            _JwtAuthenticationServiceManager = jwtAuthenticationServiceManager;
            _config = config;
            _context = _contex;
        }


        [HttpPost]
        [Route("login")]
        public String login([FromBody] Login login)
        {
            var user = _JwtAuthenticationServiceManager.Authanticate(login.Email, login.Password);
            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
            };


                var token = _JwtAuthenticationServiceManager.GenerateToken(_config["Jwt:Key"], claims);
                return token;
            }
            return "user n'existe pas ";
        }
    }
}
