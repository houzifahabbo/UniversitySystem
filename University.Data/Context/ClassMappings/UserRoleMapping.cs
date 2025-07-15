using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Data.Context.ClassMappings
{
    public class UserRoleMapping : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        void IEntityTypeConfiguration<IdentityUserRole<int>>.Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(u => new { u.UserId, u.RoleId });
        }
    }
}