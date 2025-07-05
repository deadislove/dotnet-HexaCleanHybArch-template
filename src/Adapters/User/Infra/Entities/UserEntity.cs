using Adapters.User.Infra.Entities.Bases;

namespace Adapters.User.Infra.Entities
{
    public class UserEntity: BaseEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserProfileEntity? Profile { get; set; }
    }
}
