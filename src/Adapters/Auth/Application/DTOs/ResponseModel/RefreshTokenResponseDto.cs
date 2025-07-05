namespace Adapters.Auth.Application.DTOs.ResponseModel
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
