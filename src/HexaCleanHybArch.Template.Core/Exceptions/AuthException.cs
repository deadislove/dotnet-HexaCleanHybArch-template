using HexaCleanHybArch.Template.Shared.Exceptions;

namespace HexaCleanHybArch.Template.Core.Exceptions
{
    public class AuthException: ApplicationsException
    {
        public AuthException(string message, int ErrorCode = 400) : base(message, ErrorCode)
        {
        }

        public static AuthException InvalidCredentials()
        {
            return new AuthException("Invalid email or password.", 401);
        }

        public static AuthException UserNotFound(string email)
        {
            return new AuthException($"User with email '{email}' does not exist.", 404);
        }

        public static AuthException EmailAlreadyExists(string email)
        {
            return new AuthException($"The email '{email}' is already registered.", 409);
        }

        public static AuthException RefreshTokenInvalid()
        {
            return new AuthException("Refresh token is invalid or expired.", 401);
        }

        public static AuthException TokenTampered()
        {
            return new AuthException("Token validation failed due to tampering.", 403);
        }
    }
}
