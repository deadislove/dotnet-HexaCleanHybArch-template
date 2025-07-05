using HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth;
using HexaCleanHybArch.Template.Core.DTOs.Response.Auth;

namespace HexaCleanHybArch.Template.Core.Interfaces.Auth
{
    public interface IJwtAuthService
    {
        Task<LoginCoreResponse?> Login(LoginCoreRequest loginCoreRequest);
        Task<RegisterCoreResponse?> Register(RegisterCoreRequest registerCoreRequest);
        Task<RefreshTokenCoreResponse> RefreshAccessToken(RefreshTokenCoreRequest refreshTokenCoreRequest);
    }
}
