using HexaCleanHybArch.Template.Shared.Auth;

namespace Adapters.Auth.Application.Interfaces
{
    public interface IJwtTokenService: IDisposable
    {
        string GenerateToken(JwtUserInfo user);
        string GenerateRefreshToken(JwtUserInfo user);
        JwtUserInfo? ValidateToken(string token);
        bool IsRefreshToken(string token);
    }
}
