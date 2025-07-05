using Adapters.User.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapters.User.Infra.Configurations
{
    public class UserProfileEntityTypeConfig: IEntityTypeConfiguration<UserProfileEntity>
    {
        public void Configure(EntityTypeBuilder<UserProfileEntity> builder)
        {
            builder.ToTable("UserProfiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Birthday).IsRequired();
        }
    }
}
