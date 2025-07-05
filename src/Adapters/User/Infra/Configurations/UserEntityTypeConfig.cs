using Adapters.User.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapters.User.Infra.Configurations
{
    public class UserEntityTypeConfig: IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.HasOne(x => x.Profile)
                   .WithOne(p => p.User)
                   .HasForeignKey<UserProfileEntity>(p => p.UserId);
        }
    }
}
