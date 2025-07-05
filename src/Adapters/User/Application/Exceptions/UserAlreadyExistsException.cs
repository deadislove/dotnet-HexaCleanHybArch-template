using HexaCleanHybArch.Template.Shared.Exceptions;

namespace Adapters.User.Application.Exceptions
{
    public class UserAlreadyExistsException: ApplicationsException
    {
        public override string Code => "user_exists";

        public UserAlreadyExistsException(string email): base($"User with email '{email}' already exists.") { }
    }
}
