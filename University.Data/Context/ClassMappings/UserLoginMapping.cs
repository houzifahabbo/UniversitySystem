using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Data.Context.ClassMappings
{
    public class UserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<int>>
    {
        void IEntityTypeConfiguration<IdentityUserLogin<int>>.Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
        {
            builder.ToTable("UserLogin");
            builder.HasKey(u => u.UserId);
        }
    }
}