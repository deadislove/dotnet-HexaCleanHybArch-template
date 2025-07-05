namespace Adapters.Auth.Application.DTOs.ResponseModel
{
    public class LoginResposneDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
