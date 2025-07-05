namespace HexaCleanHybArch.Template.Core.DTOs.Reqeuest.Auth
{
    public class RegisterCoreRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
