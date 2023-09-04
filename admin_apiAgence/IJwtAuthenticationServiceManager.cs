using admin_apiAgence.Models;
using System.Security.Claims;

namespace admin_apiAgence
{
    public interface IJwtAuthenticationServiceManager
    {
        admin Authanticate(String email, String password);
        String GenerateToken(String secret, List<Claim> claims);
    }
}
