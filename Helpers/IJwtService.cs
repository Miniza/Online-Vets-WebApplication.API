using System.IdentityModel.Tokens.Jwt;

namespace OnlineVetAPI.Helpers
{
    public interface IJwtService
    {
        string GenerateToken(int id);
        JwtSecurityToken VerifyToken(string jwt);
    }
}
