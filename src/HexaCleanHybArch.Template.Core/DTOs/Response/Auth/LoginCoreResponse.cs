namespace HexaCleanHybArch.Template.Core.DTOs.Response.Auth
{
    public class LoginCoreResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
