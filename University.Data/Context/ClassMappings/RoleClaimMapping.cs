using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Data.Context.ClassMappings
{
    public class RoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<int>>
    {
        void IEntityTypeConfiguration<IdentityRoleClaim<int>>.Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.ToTable("RoleClaim");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("RoleClaimId");
        }
    }
}