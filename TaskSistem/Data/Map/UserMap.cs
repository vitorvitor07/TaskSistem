using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSistem.Models;

namespace TaskSistem.Data.Map {
    public class UserMap : IEntityTypeConfiguration<UserModel> {
        void IEntityTypeConfiguration<UserModel>.Configure(EntityTypeBuilder<UserModel> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(150);

        }
    }
}