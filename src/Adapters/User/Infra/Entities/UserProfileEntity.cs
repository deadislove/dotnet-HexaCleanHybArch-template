using Adapters.User.Infra.Entities.Bases;

namespace Adapters.User.Infra.Entities
{
    public class UserProfileEntity: BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = default!;
        public DateTime Birthday { get; set; }

        public UserEntity? User { get; set; }
    }
}
