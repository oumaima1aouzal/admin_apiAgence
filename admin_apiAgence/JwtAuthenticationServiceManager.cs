using admin_apiAgence.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace admin_apiAgence
{
    public class JwtAuthenticationServiceManager : IJwtAuthenticationServiceManager
    {
        private readonly adminContext _context;
        public JwtAuthenticationServiceManager(adminContext contex)
        {
            _context = contex;
        }
        public admin Authanticate(string email, string password)
        {
            List<admin> b = _context.TableAdmin.ToList();
            var a = b.Where(u => u.Email.ToUpper().Equals(email.ToUpper())
                && u.password.Equals(password)).FirstOrDefault(); ;
            if (a != null) { return a; }
            else { return null; }
        }

        public string GenerateToken(string secret, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHnadler = new JwtSecurityTokenHandler();
            var token = tokenHnadler.CreateToken(TokenDescriptor);
            return tokenHnadler.WriteToken(token);
        }

    }
}
