using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Data.Context.ClassMappings
{
    public class RoleMapping : IEntityTypeConfiguration<IdentityRole<int>>
    {
        void IEntityTypeConfiguration<IdentityRole<int>>.Configure(EntityTypeBuilder<IdentityRole<int>> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("RoleId");
        }
    }
}